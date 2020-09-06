<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="filterText"
        placeholder="输入关键字进行过滤"
      />
    </div>
    <el-tree
      ref="orgTree"
      :data="orgTreeData"
      :props="orgTreeProps"
      :filter-node-method="filterOrg"
      :expand-on-click-node="false"
      :show-checkbox="showCheckbox"
      :check-strictly="checkStrictly"
      node-key="id"
      highlight-current
      default-expand-all
      @node-click="handleOrgClick"
      @check-change="checkChange"
    />
  </div>
</template>

<script>
import {
  getOrganizationsAllWithDetails
} from '@/api/identity/organization'
import { Tree } from 'element-ui'
export default {
  name: 'OrgTree',
  mixins: [Tree],
  props: {
    supportSingleChecked: {
      type: Boolean,
      default: false
    },
    orgTreeNodeClick: {
      type: Function,
      default: () => {}
    }
  },
  data() {
    return {
      orgTreeData: null,
      orgTreeProps: {
        label: 'displayName',
        children: 'children',
        disabled: '',
        isLeaf: 'isLeaf'
      },
      treeQuery: {
        sort: 'code',
        filter: undefined
      },
      filterText: ''
    }
  },
  watch: {
    filterText(val) {
      this.treeQuery.filter = val
      this.$refs.orgTree.filter(val)
    }
  },
  created() {
    this.getOrgs()
  },
  methods: {
    getOrgs() {
      getOrganizationsAllWithDetails(this.treeQuery).then(response => {
        this.orgTreeData = response.items
      })
    },
    handleOrgClick(data) {
      this.orgTreeNodeClick(data)
    },
    filterOrg(value, data) {
      if (!value) return true
      return data.displayName.indexOf(value) !== -1
    },
    checkChange(data, checked, indeterminate) {
      if (checked) {
        const keys = this.$refs.orgTree.getCheckedKeys()
        if (this.supportSingleChecked && keys.length > 1) {
        // this.$message({
        //   message: '只能选择一个组织',
        //   type: 'warning',
        //   showClose: true
        // })
          this.$refs.orgTree.setCheckedKeys([])
          this.$refs.orgTree.setChecked(data, true)
        }
        this.$emit('handleCheckChange', data, keys)
      }
    }
  }
}
</script>
