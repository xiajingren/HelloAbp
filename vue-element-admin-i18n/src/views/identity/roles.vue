<template>
  <div class="app-container">
    <el-row :gutter="0">
      <el-col :span="6">
        <org-tree
          ref="roleOrgTree"
          :org-tree-node-click="handleOrgTreeNodeClick"
        />
      </el-col>
      <el-col :span="18">
        <div class="filter-container">
          <el-button
            v-if="checkPermission('AbpIdentity.Roles.Create')"
            class="filter-item"
            style="margin-left: 10px;"
            type="primary"
            icon="el-icon-edit"
            @click="handleCreate"
          >
            {{ $t("AbpIdentity['NewRole']") }}
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
            :label="$t('AbpIdentity[\'RoleName\']')"
            prop="name"
            sortable
            align="center"
          >
            <template slot-scope="{ row }">
              <span>{{ row.name }}</span>
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
                v-if="checkPermission('AbpIdentity.Roles.Update')"
                type="primary"
                size="mini"
                @click="handleUpdate(row)"
              >
                {{ $t("AbpIdentity['Edit']") }}
              </el-button>
              <el-button
                v-if="checkPermission('AbpIdentity.Roles.ManagePermissions')"
                type="primary"
                size="mini"
                @click="handleUpdatePermission(row)"
              >
                {{ $t("AbpIdentity['Permissions']") }}
              </el-button>
              <el-button
                v-if="!row.isStatic && checkPermission('AbpIdentity.Roles.Delete')"
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
              ? $t('AbpIdentity[\'NewRole\']')
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
            <el-form-item :label="$t('AbpIdentity[\'OrganitaionUnits\']')" prop="orgName">
              <el-input v-model="orgName" disabled />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'RoleName\']')" prop="name">
              <el-input v-model="temp.name" :disabled="temp.isStatic" />
            </el-form-item>
            <el-form-item>
              <el-checkbox
                v-model="temp.isDefault"
                :label="$t('AbpIdentity[\'DisplayName:IsDefault\']')"
              />
            </el-form-item>
            <el-form-item>
              <el-checkbox
                v-model="temp.isPublic"
                :label="$t('AbpIdentity[\'DisplayName:IsPublic\']')"
              />
            </el-form-item>
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

        <permission-dialog ref="permissionDialog" provider-name="R" />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import {
  // getRoles,
  // createRole,
  createRoleToOrg,
  getRoleById,
  updateRole,
  deleteRole
} from '@/api/identity/role'
import {
  getOrgRoles
} from '@/api/identity/organization'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'
import PermissionDialog from './components/permission-dialog'
import OrgTree from './components/org-tree'

export default {
  name: 'Roles',
  components: { Pagination, PermissionDialog, OrgTree },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      orgData: null,
      temp: {
        orgId: '',
        name: '',
        isDefault: false,
        isPublic: false
      },
      dialogFormVisible: false,
      dialogStatus: '',
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['RoleName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['RoleName']"), '256']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {
    orgName() {
      if (this.orgData === null) {
        return ''
      }
      return `${this.orgData.displayName}(${this.orgData.code})`
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getOrgRoles(this.listQuery).then(response => {
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
      this.listQuery.ouId = undefined
      this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
      this.orgData = null
      this.handleFilter()
    },
    sortChange(data) {
      const { prop, order } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        orgId: '',
        name: '',
        isDefault: false,
        isPublic: false
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
          if (this.orgData !== null) {
            this.temp.orgId = this.orgData.id
          }
          createRoleToOrg(this.temp).then(() => {
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
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getRoleById(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateRole(this.temp).then(() => {
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
        this.$i18n.t("AbpIdentity['RoleDeletionConfirmationMessage']", [
          row.name
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"),
        {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteRole(row.id).then(() => {
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
      if (data.id) {
        this.listQuery.ouId = data.id
        this.orgData = data
        this.handleFilter()
      }
    }
  }
}
</script>
