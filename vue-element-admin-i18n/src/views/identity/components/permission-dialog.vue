<template>
  <el-dialog
    :title="
      $t('AbpPermissionManagement[\'Permissions\']') +
        ' - ' +
        permissionsQuery.providerKey
    "
    :visible.sync="dialogPermissionFormVisible"
  >
    <el-form label-position="top">
      <el-tabs tab-position="left">
        <el-tab-pane
          v-for="group in permissionData.groups"
          :key="group.name"
          :label="group.displayName"
        >
          <el-form-item :label="group.displayName">
            <el-tree
              ref="permissionTree"
              :data="transformPermissionTree(group.permissions)"
              :props="treeDefaultProps"
              show-checkbox
              check-strictly
              node-key="name"
              default-expand-all
            />
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogPermissionFormVisible = false">
        {{ $t("AbpIdentity['Cancel']") }}
      </el-button>
      <el-button type="primary" @click="updatePermissionData()">
        {{ $t("AbpIdentity['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getPermissions, updatePermissions } from '@/api/permission-management/permissions'
import { fetchAppConfig } from '@/utils/abp'

export default {
  name: 'PermissionDialog',
  props: {
    providerName: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      permissionData: {
        groups: []
      },
      treeDefaultProps: {
        children: 'children',
        label: 'label'
      },
      dialogPermissionFormVisible: false,
      permissionsQuery: { providerKey: '', providerName: '' }
    }
  },
  created() {
    this.permissionsQuery.providerName = this.providerName
  },
  methods: {
    handleUpdatePermission(row) {
      this.dialogPermissionFormVisible = true
      if (this.permissionsQuery.providerName === 'R') {
        this.permissionsQuery.providerKey = row.name
      } else if (this.permissionsQuery.providerName === 'U') {
        this.permissionsQuery.providerKey = row.id
      }

      getPermissions(this.permissionsQuery).then(response => {
        this.permissionData = response

        for (const i in this.permissionData.groups) {
          const keys = []
          const group = this.permissionData.groups[i]
          for (const j in group.permissions) {
            if (group.permissions[j].isGranted) { keys.push(group.permissions[j].name) }
          }
          this.$nextTick(() => {
            this.$refs['permissionTree'][i].setCheckedKeys(keys)
          })
        }
      })
    },
    transformPermissionTree(permissions, name = null) {
      const arr = []
      if (!permissions || !permissions.some(v => v.parentName === name)) { return arr }

      const parents = permissions.filter(v => v.parentName === name)
      for (const i in parents) {
        let label = ''
        if (this.permissionsQuery.providerName === 'R') {
          label = parents[i].displayName
        } else if (this.permissionsQuery.providerName === 'U') {
          label =
            parents[i].displayName +
            ' ' +
            parents[i].grantedProviders.map(provider => {
              return `${provider.providerName}: ${provider.providerKey}`
            })
        }
        arr.push({
          name: parents[i].name,
          label,
          disabled: this.isGrantedByOtherProviderName(
            parents[i].grantedProviders
          ),
          children: this.transformPermissionTree(permissions, parents[i].name)
        })
      }
      return arr
    },
    updatePermissionData() {
      const tempData = []
      for (const i in this.permissionData.groups) {
        const keys = this.$refs['permissionTree'][i].getCheckedKeys()
        const group = this.permissionData.groups[i]
        for (const j in group.permissions) {
          if (
            group.permissions[j].isGranted &&
            !keys.some(v => v === group.permissions[j].name)
          ) {
            tempData.push({
              isGranted: false,
              name: group.permissions[j].name
            })
          } else if (
            !group.permissions[j].isGranted &&
            keys.some(v => v === group.permissions[j].name)
          ) {
            tempData.push({ isGranted: true, name: group.permissions[j].name })
          }
        }
      }
      updatePermissions(this.permissionsQuery, { permissions: tempData }).then(
        () => {
          this.dialogPermissionFormVisible = false
          this.$notify({
            title: this.$i18n.t("HelloAbp['Success']"),
            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
          fetchAppConfig(
            this.permissionsQuery.providerKey,
            this.permissionsQuery.providerName
          )
        }
      )
    },
    isGrantedByOtherProviderName(grantedProviders) {
      if (grantedProviders.length) {
        return (
          grantedProviders.findIndex(
            p => p.providerName !== this.permissionsQuery.providerName
          ) > -1
        )
      }
      return false
    }
  }
}
</script>
