<template>
  <div class="app-container">
    <el-row :gutter="24">
      <el-col :span="10">
        <el-card>
          <div slot="header" class="clearfix">
            <span>{{ $t("AbpDataDictionary['DataDictionary']") }}</span>
          </div>
          <div class="text item">
            <div class="filter-container">
              <el-form v-model="listQuery" label-width="80px">
                <el-col :span="14">
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Create')"
                    type="primary"
                    icon="el-icon-plus"
                    size="small"
                    @click="handleCreate"
                  >
                    {{ $t("AbpDataDictionary['Create']") }}
                  </el-button>
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Delete')"
                    type="danger"
                    icon="el-icon-delete"
                    size="small"
                    @click="handleDelete"
                  >
                    {{ $t("AbpDataDictionary['Delete']") }}
                  </el-button>
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Update')"
                    type="primary"
                    icon="el-icon-edit"
                    size="small"
                    @click="handleUpdate"
                  >
                    {{ $t("AbpDataDictionary['Edit']") }}
                  </el-button>
                </el-col>
                <el-col :span="10">
                  <el-form-item
                    :label="$t('AbpDataDictionary[\'Filter\']')+':'"
                    prop="filter"
                  >
                    <el-input v-model="listQuery.filter" @keyup.enter.native="handleFilter" />
                  </el-form-item>
                </el-col>
              </el-form>
            </div>
            <el-table
              v-loading="listLoading"
              :data="list"
              border
              fit
              highlight-current-row
              style="width: 100%;"
              @row-click="handleContentClick"
              @sort-change="handleListSortChange"
              @selection-change="handleListSelectionChange"
            >
              <el-table-column
                type="selection"
                width="55"
              />
              <el-table-column
                :label="$t('AbpDataDictionary[\'Name\']')"
                prop="name"
                align="center"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.name }}</span>
                </template>
              </el-table-column>
              <el-table-column
                :label="$t('AbpDataDictionary[\'Description\']')"
                prop="description"
                align="center"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.description }}</span>
                </template>
              </el-table-column>
              <el-table-column
                :label="$t('AbpDataDictionary[\'Sort\']')"
                sortable="custom"
                prop="sort"
                align="center"
                width="80"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.sort }}</span>
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
        </el-card>

      </el-col>
      <el-col :span="14">
        <el-card style="height:100%">
          <div slot="header" class="clearfix">
            <span>{{ $t("AbpDataDictionary['DataDictionaryDetail']") }}</span>
          </div>
          <div class="text item">
            <div class="filter-container">
              <el-form v-model="listDetailsQuery" label-position="right" label-width="80px">
                <el-col :span="12">
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Detail.Create')"
                    type="primary"
                    icon="el-icon-plus"
                    size="small"
                    @click="handleCreateDetail"
                  >
                    {{ $t("AbpDataDictionary['Create']") }}
                  </el-button>
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Detail.Delete')"
                    type="danger"
                    icon="el-icon-delete"
                    size="small"
                    @click="handleDeleteDetail"
                  >
                    {{ $t("AbpDataDictionary['Delete']") }}
                  </el-button>
                  <el-button
                    v-if="checkPermission('AbpDataDictionary.Detail.Update')"
                    type="primary"
                    icon="el-icon-edit"
                    size="small"
                    @click="handleDetailUpdate"
                  >
                    {{ $t("AbpDataDictionary['Edit']") }}
                  </el-button>
                </el-col>
                <el-col :span="12">
                  <el-form-item
                    :label="$t('AbpDataDictionary[\'Filter\']')+':'"
                    prop="filter"
                  >
                    <el-input v-model="listDetailsQuery.filter" @keyup.enter.native="handleFilterDetail" />
                  </el-form-item>
                </el-col>
              </el-form>
            </div>
            <el-table
              v-loading="listLoadingDetails"
              :data="listDetails"
              border
              fit
              highlight-current-row
              style="width: 100%;"
              @row-click="currentDetailData = $event"
              @sort-change="handleListDetailsSortChange"
              @selection-change="handleDetailsListSelectionChange"
            >
              <el-table-column
                type="selection"
                width="55"
              />
              <el-table-column
                :label="$t('AbpDataDictionary[\'Label\']')"
                prop="label"
                align="center"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.label }}</span>
                </template>
              </el-table-column>
              <el-table-column
                :label="$t('AbpDataDictionary[\'Value\']')"
                prop="value"
                align="center"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.value }}</span>
                </template>
              </el-table-column>
              <el-table-column
                :label="$t('AbpDataDictionary[\'Sort\']')"
                sortable
                prop="sort"
                align="center"
                width="80"
              >
                <template slot-scope="{ row }">
                  <span>{{ row.sort }}</span>
                </template>
              </el-table-column>
            </el-table>
            <pagination
              v-show="totalDetails > 0"
              :total="totalDetails"
              :page.sync="listDetailsQuery.page"
              :limit.sync="listDetailsQuery.limit"
              @pagination="getDetailsList"
            />
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-dialog
      :title="dialogStatus==='create'?
        $t('AbpDataDictionary[\'NewDataDic\']') :
        $t('AbpDataDictionary[\'Edit\']')+'-'+currentContentData.name "
      :visible="dialogFormVisible"
    >
      <el-form
        ref="dataFormContent"
        :rules="rules"
        :model="tempContent"
        label-position="right"
        label-width="80px"
      >
        <el-form-item
          :label="$t('AbpDataDictionary[\'Name\']')"
          prop="name"
        >
          <el-input v-model="tempContent.name" />
        </el-form-item>
        <el-form-item
          :label="$t('AbpDataDictionary[\'Sort\']')"
          prop="sort"
        >
          <el-input v-model="tempContent.sort" type="number" />
        </el-form-item>
        <el-form-item
          :label="$t('AbpDataDictionary[\'Description\']')"
          prop="description"
        >
          <el-input
            v-model="tempContent.description"
            type="textarea"
            maxlength="256"
            show-word-limit
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpDataDictionary['Cancel']") }}
        </el-button>
        <el-button
          type="primary"
          @click="dialogStatus==='create'?
           createData() :
           updateData() "
        >
          {{ $t("AbpDataDictionary['Save']") }}
        </el-button>
      </div>
    </el-dialog>

    <el-dialog
      :title="currentContentData.name+'--'+(
        dialogDetailStatus==='create'?
          $t('AbpDataDictionary[\'NewDataDicDetail\']') :
          $t('AbpDataDictionary[\'Edit\']')+'-'+currentDetailData.label)"
      :visible="dialogFormDetailVisible"
    >
      <el-form
        ref="dataFormDetail"
        :rules="rules"
        :model="tempDetail"
        label-position="right"
        label-width="80px"
      >
        <el-form-item
          :label="$t('AbpDataDictionary[\'Label\']')"
          prop="label"
        >
          <el-input v-model="tempDetail.label" />
        </el-form-item>
        <el-form-item
          :label="$t('AbpDataDictionary[\'Value\']')"
          prop="value"
        >
          <el-input
            v-model="tempDetail.value"
          />
        </el-form-item>
        <el-form-item
          :label="$t('AbpDataDictionary[\'Sort\']')"
          prop="sort"
        >
          <el-input v-model="tempDetail.sort" type="number" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormDetailVisible = false">
          {{ $t("AbpDataDictionary['Cancel']") }}
        </el-button>
        <el-button
          type="primary"
          @click="dialogDetailStatus==='create'?
           createDetailData() :
           updateDetailData() "
        >
          {{ $t("AbpDataDictionary['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getDatationaryList,
  getDatationaryDetailList,
  updateDatationary,
  updateDatationaryDetail,
  createDatadictionary,
  createDatadictionaryDetail,
  deleteDatationaries,
  deleteDatationariesDetail
} from '@/api/data-dictionary/dictionaries'
import Pagination from '@/components/Pagination'
import baseListQuery, { checkPermission } from '@/utils/abp'
import _ from 'lodash'

export default {
  name: 'DataDictionary',
  components: { Pagination },
  data() {
    return {
      dialogFormVisible: false,
      dialogFormDetailVisible: false,
      dialogStatus: 'create',
      dialogDetailStatus: 'create',
      list: null,
      listDetails: null,
      total: 0,
      totalDetails: 0,
      listLoading: true,
      listLoadingDetails: false,
      listQuery: baseListQuery,
      listDetailsQuery: {
        ...baseListQuery,
        ...{ pid: '' }
      },
      selectDetails: [],
      selectContent: [],
      tempContent: {
        name: '',
        sort: 0,
        description: ''
      },
      tempDetail: {
        label: '',
        sort: 0,
        value: '',
        pid: ''
      },
      currentContentData: {},
      currentDetailData: {},
      rules: {

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
      getDatationaryList(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    getDetailsList() {
      this.listLoadingDetails = true
      getDatationaryDetailList(this.listDetailsQuery).then(response => {
        this.listDetails = response.items
        this.totalDetails = response.totalCount
        this.listLoadingDetails = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    handleFilterDetail(firstPage = true) {
      if (firstPage) this.listDetailsQuery.page = 1
      this.getDetailsList()
    },
    handleCreate() {
      this.resetTempContent()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataFormContent'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataFormContent'].validate(valid => {
        if (valid) {
          createDatadictionary(this.tempContent).then(response => {
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
    handleUpdate() {
      if (Object.keys(this.currentContentData).length === 0) {
        this.$notify({
          title: this.$i18n.t('dataDictionary.warning'),
          message: this.$i18n.t('dataDictionary.unSelectDictionaryMessage'),
          type: 'warning',
          duration: 2000
        })
        return
      }
      this.dialogStatus = 'edit'
      this.resetTempContent()
      this.tempContent = _.pick(this.currentContentData, 'id', 'name', 'description', 'sort')
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataFormContent'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataFormContent'].validate(valid => {
        if (valid) {
          const id = this.tempContent.id
          delete this.tempContent.id
          console.log('id:', id)
          updateDatationary(id, this.tempContent).then(response => {
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
    handleCreateDetail() {
      if (Object.keys(this.currentContentData).length === 0) {
        this.$notify({
          title: this.$i18n.t('dataDictionary.warning'),
          message: this.$i18n.t('dataDictionary.unSelectDictionaryMessage'),
          type: 'warning',
          duration: 2000
        })
        return
      }
      this.dialogDetailStatus = 'create'
      this.resetTempDetail()
      this.dialogFormDetailVisible = true
      this.$nextTick(() => {
        this.$refs['dataFormDetail'].clearValidate()
      })
    },
    createDetailData() {
      this.$refs['dataFormDetail'].validate(valid => {
        if (valid) {
          this.tempDetail.pid = this.currentContentData.id
          createDatadictionaryDetail(this.tempDetail).then(response => {
            this.handleFilterDetail()
            this.dialogFormDetailVisible = false
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
    handleDetailUpdate() {
      if (Object.keys(this.currentDetailData).length === 0) {
        this.$notify({
          title: this.$i18n.t('dataDictionary.warning'),
          message: this.$i18n.t('dataDictionary.unSelectDictionaryMessage'),
          type: 'warning',
          duration: 2000
        })
        return
      }
      this.dialogDetailStatus = 'edit'
      this.resetTempDetail()
      this.tempDetail = _.pick(this.currentDetailData, 'id', 'value', 'label', 'sort')
      this.dialogFormDetailVisible = true
      this.$nextTick(() => {
        this.$refs['dataFormDetail'].clearValidate()
      })
    },
    updateDetailData() {
      this.$refs['dataFormDetail'].validate(valid => {
        if (valid) {
          // 这里拿出来自我感觉是为了不要在入参中携带脏数据
          const id = this.tempDetail.id
          delete this.tempDetail.id
          updateDatationaryDetail(id, this.tempDetail).then(response => {
            this.handleFilterDetail(false)
            this.dialogFormDetailVisible = false
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
    handleDelete() {
      if (this.selectContent.length === 0) {
        this.$notify({
          title: this.$i18n.t('dataDictionary.warning'),
          message: this.$i18n.t('dataDictionary.unCheckedDictionaryMessage'),
          type: 'warning',
          duration: 2000
        })
        return
      }
      deleteDatationaries(this.selectContent).then(() => {
        this.handleFilter()
        // 清空字典值
        this.listDetails = null
        this.$notify({
          title: this.$i18n.t("HelloAbp['Success']"),
          message: this.$i18n.t('dataDictionary.deleteSuccess'),
          type: 'success',
          duration: 2000
        })
      })
    },
    handleDeleteDetail() {
      if (this.selectDetails.length === 0) {
        this.$notify({
          title: this.$i18n.t('dataDictionary.warning'),
          message: this.$i18n.t('dataDictionary.unCheckedDictionaryMessage'),
          type: 'warning',
          duration: 2000
        })
        return
      }
      deleteDatationariesDetail(this.selectDetails).then(() => {
        this.handleFilterDetail()
        this.$notify({
          title: this.$i18n.t("HelloAbp['Success']"),
          message: this.$i18n.t('dataDictionary.deleteSuccess'),
          type: 'success',
          duration: 2000
        })
      })
    },
    resetTempContent() {
      this.tempContent = {
        name: '',
        sort: 0,
        description: ''
      }
    },
    resetTempDetail() {
      this.tempDetail = {
        label: '',
        sort: 0,
        value: '',
        pid: ''
      }
    },
    handleContentClick(row) {
      this.listDetailsQuery.pid = row.id
      this.currentContentData = row
      this.handleFilterDetail()
    },
    handleListSortChange(e) {
      const { prop, order } = e
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleListDetailsSortChange(e) {
      const { prop, order } = e
      this.listDetailsQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilterDetail()
    },
    handleListSelectionChange(val) {
      this.selectContent = val.map(d => d.id)
    },
    handleDetailsListSelectionChange(val) {
      this.selectDetails = val.map(d => d.id)
    }
  }
}
</script>
