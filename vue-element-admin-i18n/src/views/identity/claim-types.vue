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
        v-if="checkPermission('AbpIdentity.ClaimTypes.Create')"
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-edit"
        @click="handleCreate"
      >
        {{ $t("AbpIdentity['NewClaimType']") }}
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
        :label="$t('AbpIdentity[\'ClaimName\']')"
        prop="name"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'ClaimValueType\']')"
        prop="valueType"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.valueTypeAsString }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'ClaimDescription\']')"
        prop="description"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'ClaimRegex\']')"
        prop="regex"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.regex }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('AbpIdentity[\'Actions\']')"
        align="center"
        width="200"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row, $index }">
          <el-button
            v-if="
              !row.isStatic && checkPermission('AbpIdentity.ClaimTypes.Update')
            "
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
          >
            {{ $t("AbpIdentity['Edit']") }}
          </el-button>
          <el-button
            v-if="checkPermission('AbpIdentity.ClaimTypes.Delete')"
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
          ? $t('AbpIdentity[\'NewClaimType\']')
          : $t('AbpIdentity[\'Edit\']')
      "
      :visible.sync="dialogFormVisible"
    >
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-position="right"
        label-width="150px"
      >
        <el-form-item :label="$t('AbpIdentity[\'ClaimName\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item>
          <el-checkbox
            v-model="temp.required"
            :label="$t('AbpIdentity[\'ClaimRequired\']')"
          />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'ClaimRegex\']')" prop="regex">
          <el-input v-model="temp.regex" />
        </el-form-item>
        <el-form-item
          :label="$t('AbpIdentity[\'ClaimRegexDescription\']')"
          prop="regexDescription"
        >
          <el-input v-model="temp.regexDescription" />
        </el-form-item>
        <el-form-item
          :label="$t('AbpIdentity[\'ClaimDescription\']')"
          prop="description"
        >
          <el-input v-model="temp.description" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'ClaimValueType\']')">
          <el-select v-model="temp.valueType">
            <el-option :value="0" label="String" />
            <el-option :value="1" label="Int" />
            <el-option :value="2" label="Boolean" />
            <el-option :value="3" label="DateTime" />
          </el-select>
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
  </div>
</template>

<script>
import {
  getClaimTypes,
  getClaimTypeById,
  // getClaimTypesAll,
  createClaimType,
  updateClaimType,
  deleteClaimType
} from '@/api/identity/claim-type'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'

export default {
  name: 'ClaimTypes',
  components: {
    Pagination
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        name: '',
        required: false,
        regex: '',
        regexDescription: '',
        description: '',
        valueType: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['ClaimName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['ClaimName']"), '256']
            ),
            trigger: 'blur'
          }
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
      getClaimTypes(this.listQuery).then(response => {
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
        name: '',
        required: false,
        regex: '',
        regexDescription: '',
        description: '',
        valueType: 0
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
          createClaimType(this.temp).then(() => {
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

      getClaimTypeById(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateClaimType(this.temp).then(() => {
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
        this.$i18n.t("AbpIdentity['ClaimDeletionConfirmationMessage']", [
          row.name
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"),
        {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteClaimType(row.id).then(() => {
          this.handleFilter()
          this.$notify({
            title: this.$i18n.t("HelloAbp['Success']"),
            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    }
  }
}
</script>
