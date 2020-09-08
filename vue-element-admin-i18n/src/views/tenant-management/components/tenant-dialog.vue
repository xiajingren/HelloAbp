<template>
  <el-dialog
    :title="
      dialogStatus == 'create'
        ? $t('AbpTenantManagement[\'NewTenant\']')
        : $t('AbpTenantManagement[\'Edit\']')
    "
    :visible.sync="dialogFormVisible"
  >
    <el-form
      ref="dataForm"
      :rules="rules"
      :model="temp"
      label-position="right"
      label-width="180px"
    >
      <el-form-item
        :label="$t('AbpTenantManagement[\'TenantName\']')"
        prop="name"
      >
        <el-input v-model="temp.name" />
      </el-form-item>
      <el-form-item
        v-if="!temp.id"
        :label="$t('AbpTenantManagement[\'DisplayName:AdminEmailAddress\']')"
        prop="adminEmailAddress"
      >
        <el-input v-model="temp.adminEmailAddress" />
      </el-form-item>
      <el-form-item
        v-if="!temp.id"
        :label="$t('AbpTenantManagement[\'DisplayName:AdminPassword\']')"
        prop="adminPassword"
      >
        <el-input
          v-model="temp.adminPassword"
          type="password"
          auto-complete="off"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogFormVisible = false">
        {{ $t("AbpTenantManagement['Cancel']") }}
      </el-button>
      <el-button
        type="primary"
        @click="dialogStatus === 'create' ? createData() : updateData()"
      >
        {{ $t("AbpTenantManagement['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  createTenant,
  getTenantById,
  updateTenant
} from '@/api/multi-tenancy/tenant'

export default {
  name: 'TenantDialog',
  data() {
    const passwordValidator = (rule, value, callback) => {
      if (!value) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpTenantManagement['DisplayName:AdminPassword']")
            ])
          )
        )
        return
      }
      if (value.length < 6) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordTooShort']", ['6'])
          )
        )
        return
      }
      if (value.length > 128) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:AdminPassword']"
                ),
                '128'
              ]
            )
          )
        )
        return
      }
      let reg = /\d+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresDigit']")
          )
        )
        return
      }
      reg = /[a-z]+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresLower']")
          )
        )
        return
      }
      reg = /[A-Z]+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresUpper']")
          )
        )
        return
      }
      reg = /\W+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['Identity.PasswordRequiresNonAlphanumeric']"
            )
          )
        )
        return
      }

      callback()
    }
    return {
      temp: {
        name: ''
      },
      dialogStatus: '',
      dialogFormVisible: false,
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t(
              "AbpTenantManagement['The {0} field is required.']",
              [this.$i18n.t("AbpTenantManagement['TenantName']")]
            ),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpTenantManagement['TenantName']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        adminEmailAddress: [
          {
            required: true,
            message: this.$i18n.t(
              "AbpTenantManagement['The {0} field is required.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:AdminEmailAddress']"
                )
              ]
            ),
            trigger: 'blur'
          },
          {
            type: 'email',
            message: this.$i18n.t(
              "AbpTenantManagement['The {0} field is not a valid e-mail address.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:AdminEmailAddress']"
                )
              ]
            ),
            trigger: ['blur', 'change']
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:AdminEmailAddress']"
                ),
                '256'
              ]
            ),
            trigger: 'blur'
          }
        ],
        adminPassword: [
          {
            required: true,
            validator: passwordValidator,
            trigger: ['blur', 'change']
          }
        ]
      }
    }
  },
  methods: {
    resetTemp() {
      this.temp = {
        name: ''
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createTenant(this.temp).then(() => {
            this.$emit('handleFilter')
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("HelloAbp['Success']"),
              message: this.$i18n.t("HelloAbp['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getTenantById(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateTenant(this.temp).then(() => {
            this.$emit('handleFilter', false)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("HelloAbp['Success']"),
              message: this.$i18n.t("HelloAbp['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    }
  }
}
</script>
