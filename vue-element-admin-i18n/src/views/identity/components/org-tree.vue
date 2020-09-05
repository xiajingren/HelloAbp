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
      node-key="id"
      highlight-current
      default-expand-all
      @node-click="handleOrgClick"
    />
  </div>
</template>

<script>
import {
  getOrganizationsAllWithDetails
} from '@/api/identity/organization'
export default {
  name: 'OrgTree',
  props: {
    orgTreeNodeClick: {
      type: Function,
      default: null
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
    }
  }
}
</script>
