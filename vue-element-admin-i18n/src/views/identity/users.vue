<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.filter"
        :placeholder="$t('AbpUi[\'PagerSearch\']')"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-button
        v-if="checkPermission('AbpIdentity.Users.Create')"
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-edit"
        @click="handleCreate"
      >
        {{ $t("AbpIdentity['NewUser']") }}
      </el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column
        :label="$t('AbpIdentity[\'UserName\']')"
        prop="userName"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.userName }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'EmailAddress\']')"
        prop="email"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'PhoneNumber\']')"
        prop="phoneNumber"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.phoneNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'Actions\']')"
        align="center"
        width="300"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row, $index }">
          <el-button
            v-if="checkPermission('AbpIdentity.Users.Update')"
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
          >
            {{ $t("AbpIdentity['Edit']") }}
          </el-button>
          <el-button
            v-if="checkPermission('AbpIdentity.Users.ManagePermissions')"
            type="primary"
            size="mini"
            @click="handleUpdatePermission(row)"
          >
            {{ $t("AbpIdentity['Permissions']") }}
          </el-button>
          <el-button
            v-if="checkPermission('AbpIdentity.Users.Delete')"
            size="mini"
            type="danger"
            @click="handleDelete(row, $index)"
          >
            {{ $t("AbpIdentity['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getList"
    />

    <el-dialog
      :title="
        dialogStatus == 'create'
          ? $t('AbpIdentity[\'NewUser\']')
          : $t('AbpIdentity[\'Edit\']')
      "
      :visible.sync="dialogFormVisible"
    >
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-position="right"
        label-width="120px"
      >
        <el-tabs tab-position="top">
          <el-tab-pane :label="$t('AbpIdentity[\'UserInformations\']')">
            <el-form-item
              :label="$t('AbpIdentity[\'UserName\']')"
              prop="userName"
            >
              <el-input v-model="temp.userName" />
            </el-form-item>
            <el-form-item
              :label="$t('AbpIdentity[\'DisplayName:Name\']')"
              prop="name"
            >
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item
              :label="$t('AbpIdentity[\'DisplayName:Surname\']')"
              prop="surname"
            >
              <el-input v-model="temp.surname" />
            </el-form-item>
            <el-form-item
              :label="$t('AbpIdentity[\'Password\']')"
              prop="password"
              :class="{ 'is-required': !temp.id }"
            >
              <el-input
                v-model="temp.password"
                type="password"
                auto-complete="off"
              />
            </el-form-item>
            <el-form-item
              :label="$t('AbpIdentity[\'EmailAddress\']')"
              prop="email"
            >
              <el-input v-model="temp.email" />
            </el-form-item>
            <el-form-item
              :label="$t('AbpIdentity[\'PhoneNumber\']')"
              prop="phoneNumber"
            >
              <el-input v-model="temp.phoneNumber" />
            </el-form-item>
            <el-form-item prop="lockoutEnabled">
              <el-checkbox
                v-model="temp.lockoutEnabled"
                :label="$t('AbpIdentity[\'DisplayName:LockoutEnabled\']')"
              />
            </el-form-item>
            <el-form-item prop="twoFactorEnabled">
              <el-checkbox
                v-model="temp.twoFactorEnabled"
                :label="$t('AbpIdentity[\'DisplayName:TwoFactorEnabled\']')"
              />
            </el-form-item>
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentity[\'Roles\']')">
            <el-form-item>
              <el-checkbox-group v-model="temp.roleNames">
                <el-checkbox
                  v-for="role in assignableRoles"
                  :key="role.id"
                  :label="role.name"
                  name="roleName"
                  style="width:100%;"
                />
              </el-checkbox-group>
            </el-form-item>
          </el-tab-pane>
        </el-tabs>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpIdentity['Cancel']") }}
        </el-button>
        <el-button
          type="primary"
          @click="dialogStatus === 'create' ? createData() : updateData()"
        >
          {{ $t("AbpIdentity['Save']") }}
        </el-button>
      </div>
    </el-dialog>

    <permission-dialog ref="permissionDialog" provider-name="U" />
  </div>
</template>

<script>
import {
  getUsers,
  createUser,
  getUserById,
  updateUser,
  deleteUser,
  getRolesByUserId,
  getAssignableRoles
} from '@/api/identity/user'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'
import PermissionDialog from './components/permission-dialog'

export default {
  name: 'Users',
  components: { Pagination, PermissionDialog },
  data() {
    const passwordValidator = (rule, value, callback) => {
      if (this.temp.id && !value) {
        callback()
        return
      }

      if (!value) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['Password']")
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
              [this.$i18n.t("AbpIdentity['Password']"), '128']
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
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        userName: '',
        email: '',
        name: '',
        surname: '',
        phoneNumber: '',
        lockoutEnabled: true,
        twoFactorEnabled: true,
        roleNames: []
      },
      dialogFormVisible: false,
      dialogStatus: '',
      assignableRoles: null,
      rules: {
        userName: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['UserName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['UserName']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        email: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['EmailAddress']")
            ]),
            trigger: 'blur'
          },
          {
            type: 'email',
            message: this.$i18n.t(
              "AbpIdentity['The {0} field is not a valid e-mail address.']",
              [this.$i18n.t("AbpIdentity['EmailAddress']")]
            ),
            trigger: ['blur', 'change']
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['EmailAddress']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        name: [
          {
            max: 64,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        surname: [
          {
            max: 64,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['DisplayName:Surname']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        phoneNumber: [
          {
            max: 16,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['PhoneNumber']"), '16']
            ),
            trigger: 'blur'
          }
        ],
        password: [
          { validator: passwordValidator, trigger: ['blur', 'change'] }
        ]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getUsers(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount

        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const { prop, order } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        userName: '',
        email: '',
        name: '',
        surname: '',
        phoneNumber: '',
        lockoutEnabled: true,
        twoFactorEnabled: true,
        roleNames: []
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true

      getAssignableRoles().then(response => {
        this.assignableRoles = response.items
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createUser(this.temp).then(() => {
            this.handleFilter()
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
      this.temp = Object.assign({ roleNames: [] }, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getUserById(row.id).then(response => {
        this.temp = Object.assign({ roleNames: [] }, response)

        getRolesByUserId(row.id).then(response => {
          response.items.forEach(item => {
            this.temp.roleNames.push(item.name)
          })
        })
      })

      getAssignableRoles().then(response => {
        this.assignableRoles = response.items
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateUser(this.temp).then(() => {
            this.handleFilter(false)
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
    handleDelete(row, index) {
      this.$confirm(
        this.$i18n.t("AbpIdentity['UserDeletionConfirmationMessage']", [
          row.userName
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"),
        {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteUser(row.id).then(() => {
          this.handleFilter()
          this.$notify({
            title: this.$i18n.t("HelloAbp['Success']"),
            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleUpdatePermission(row) {
      this.$refs['permissionDialog'].handleUpdatePermission(row)
    }
  }
}
</script>
