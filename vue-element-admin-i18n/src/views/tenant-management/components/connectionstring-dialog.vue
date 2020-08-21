<template>
  <el-dialog
    :title="$t('AbpTenantManagement[\'ConnectionStrings\']')"
    :visible.sync="dialogFormVisible"
  >
    <el-form
      ref="dataForm"
      :rules="rules"
      :model="temp"
      label-position="right"
      label-width="200px"
    >
      <el-form-item>
        <el-checkbox
          v-model="useSharedDatabase"
          :label="$t('AbpTenantManagement[\'DisplayName:UseSharedDatabase\']')"
        />
      </el-form-item>
      <el-form-item
        v-if="!useSharedDatabase"
        :label="
          $t('AbpTenantManagement[\'DisplayName:DefaultConnectionString\']')
        "
        prop="defaultConnectionString"
      >
        <el-input v-model="temp.defaultConnectionString" />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogFormVisible = false">
        {{ $t("AbpTenantManagement['Cancel']") }}
      </el-button>
      <el-button type="primary" @click="updateData()">
        {{ $t("AbpTenantManagement['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  getDefaultConnectionString,
  updateDefaultConnectionString,
  deleteDefaultConnectionString
} from '@/api/multi-tenancy/tenant'

export default {
  name: 'ConnectionstringDialog',
  data() {
    return {
      tenantId: '',
      useSharedDatabase: true,
      temp: {
        defaultConnectionString: ''
      },
      dialogFormVisible: false,
      rules: {
        defaultConnectionString: [
          {
            max: 1024,
            message: this.$i18n.t(
              "AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:DefaultConnectionString']"
                ),
                '1024'
              ]
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  methods: {
    resetTemp() {
      this.temp = {
        defaultConnectionString: ''
      }
    },
    handleUpdate(row) {
      this.resetTemp()
      this.tenantId = row.id
      this.dialogFormVisible = true

      getDefaultConnectionString(row.id).then(response => {
        if (response) {
          this.useSharedDatabase = false
          this.temp.defaultConnectionString = response
        } else {
          this.useSharedDatabase = true
        }
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          if (
            this.useSharedDatabase ||
            (!this.useSharedDatabase && !this.temp.defaultConnectionString)
          ) {
            deleteDefaultConnectionString(this.tenantId).then(() => {
              this.dialogFormVisible = false
              this.$notify({
                title: this.$i18n.t("HelloAbp['Success']"),
                message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            })
          } else {
            updateDefaultConnectionString(this.tenantId, this.temp).then(() => {
              this.dialogFormVisible = false
              this.$notify({
                title: this.$i18n.t("HelloAbp['Success']"),
                message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            })
          }
        }
      })
    }
  }
}
</script>
