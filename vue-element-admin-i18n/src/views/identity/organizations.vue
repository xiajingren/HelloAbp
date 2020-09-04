<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button
        v-if="checkPermission('AbpIdentity.OrganitaionUnits.Create')"
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-edit"
        @click="handleCreate"
      >
        {{ $t('AbpIdentity[\'NewOrganitaionUnit\']') }}
      </el-button>
    </div>
    <el-table
      :data="list"
      style="width: 100%"
      row-key="id"
      border
      fit
      highlight-current-row
      lazy
      :load="loadChildren"
      :tree-props="{children: 'children', hasChildren: 'isLeaf'}"
    >
      <el-table-column
        type="selection"
        width="55"
      />
      <el-table-column
        label="编码"
        prop="code"
      >
        <template slot-scope="{ row }">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="名称"
        prop="displayName"
      >
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button
            size="mini"
            @click="handleEdit(scope.$index, scope.row)"
          >Edit</el-button>
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
          >Delete</el-button>
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

  </div>
</template>

<script>
import {
  getOrganizationsRoot,
  getOrganizationChildren,
  createOrganization,
  updateOrganization,
  deleteOrganization
} from '@/api/identity/organization'
import Pagination from '@/components/Pagination'
import baseListQuery, { checkPermission } from '@/utils/abp'
export default {
  name: 'Organizations',
  components: { Pagination },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        parentId: '',
        displayName: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      assignableRoles: null
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getOrganizationsRoot(this.listQuery).then(response => {
        this.list = response.items.map(item => {
          item.children = []
          item.isLeaf = !item.isLeaf
          return item
        })
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    loadChildren(row, treeNode, resolve) {
      getOrganizationChildren(row.id).then(response => {
        const data = response.map(res => {
          res.isLeaf = !res.isLeaf
          return res
        })
        resolve(data)
      })
    },
    createData() {
      createOrganization()
    },
    updateData(row) {
      updateOrganization()
    },
    deleteData(row) {
      deleteOrganization()
    }
  }
}
</script>
