<template>
  <div class="app-container">
    <el-row :gutter="0">
      <el-col :span="6">
        <org-tree
          ref="userOrgTree"
          :org-tree-node-click="handleOrgTreeNodeClick"
        />
      </el-col>
      <el-col :span="18">
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
          <el-button
            class="filter-item"
            style="margin-left: 10px;"
            icon="el-icon-refresh"
            @click="handleRefresh"
          >
            {{ $t("AbpIdentity['Refresh']") }}
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
              <el-tab-pane :label="$t('AbpIdentity[\'OrganitaionUnits\']')">
                <el-form-item>
                  <org-tree
                    ref="dialogOrgTree"
                    :show-checkbox="true"
                    :check-strictly="true"
                    :support-single-checked="singleChecked"
                    @handleCheckChange="handleCheckChange"
                  />
                </el-form-item>
              </el-tab-pane>
            </el-tabs>
          </el-form>
          <div
            slot="footer"
            class="dialog-footer"
          >
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

        <permission-dialog
          ref="permissionDialog"
          provider-name="U"
        />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import {
  // getUsers,
  createUserToOrg,
  getUserById,
  // updateUser,
  updateUserToOrg,
  deleteUser,
  getRolesByUserId,
  getAssignableRoles,
  getOrganizationsByUserId
} from '@/api/identity/user'
import {
  getOrgUsers
} from '@/api/identity/organization'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'
import PermissionDialog from './components/permission-dialog'
import OrgTree from './components/org-tree'

export default {
  name: 'Users',
  components: { Pagination, PermissionDialog, OrgTree },
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
      if (value.length < this.requiredLength) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordTooShort']", [`${this.requiredLength}`])
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
      if (this.requireDigit && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresDigit']")
          )
        )
        return
      }
      reg = /[a-z]+/
      if (this.requireLowercase && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresLower']")
          )
        )
        return
      }
      reg = /[A-Z]+/
      if (this.requireUppercase && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresUpper']")
          )
        )
        return
      }
      reg = /\W+/
      if (this.requireNonAlphanumeric && !reg.test(value)) {
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
      singleChecked: false,
      temp: {
        orgIds: [],
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
  computed: {
    requiredLength() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequiredLength'] || 6
    },
    requiredUniqueChars() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequiredUniqueChars'] || 1
    },
    requireLowercase() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequireLowercase'] || true
    },
    requireNonAlphanumeric() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequireNonAlphanumeric'] || true
    },
    requireUppercase() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequireUppercase'] || true
    },
    requireDigit() {
      return this.$store.state.app.abpConfig.setting.values['Abp.Identity.Password.RequireDigit'] || true
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getOrgUsers(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount

        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    handleRefresh() {
      this.listQuery.filter = undefined
      this.getList()
    },
    sortChange(data) {
      const { prop, order } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        orgIds: [],
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
      // create org
      this.singleChecked = false

      getAssignableRoles().then(response => {
        this.assignableRoles = response.items
      })

      this.$nextTick(() => {
        this.$refs.dialogOrgTree.$refs.orgTree.setCheckedKeys([])
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createUserToOrg(this.temp).then(() => {
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
      // update can check many
      this.singleChecked = false

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

      // handle org
      getOrganizationsByUserId(row.id).then(response => {
        const ids = this.temp.orgIds = response.items.map(item => item.id)
        // 2.bind checked
        this.$refs.dialogOrgTree.$refs.orgTree.setCheckedKeys(ids)
      })

      this.$nextTick(() => {
        // 1.reset dialog tree
        this.$refs.dialogOrgTree.$refs.orgTree.setCheckedKeys([])
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateUserToOrg(this.temp).then(() => {
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
    },
    handleOrgTreeNodeClick(data) {
      this.listQuery.ouId = data.id
      this.handleFilter()
    },
    handleCheckChange(data, orgIds) {
      // singleChecked
      if (orgIds) {
        this.temp.orgIds = orgIds
      }
    }
  }
}
</script>
