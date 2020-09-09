<template>
  <div class="app-container">
    <div class="filter-container">
      <el-container>
        <form ref="logQueryForm" :model="queryForm">
          <el-row>
            <el-col>
              <el-date-picker
                v-model="queryForm.startTime"
                type="datetimerange"
                :picker-options="pickerOptions"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                align="right"
              />
            </el-col>
          </el-row>
        </form>
      </el-container>
    </div>

    <div class="table-container">

      <el-table
        :key="tableKey"
        v-loading="listLoading"
        :data="list"
        border
        highlight-current-row
        style="width: 100%;"
      >
        <el-table-column
          :label="$t('AbpAuditLogging[\'Url\']')"
          prop="url"
          align="center"
        >
          <template slot-scope="{ row }">
            <span>{{ row.url }}</span>
          </template>
        </el-table-column>
        <el-table-column
          :label="$t('AbpAuditLogging[\'HttpMethod\']')"
          prop="httpMethod"
          align="center"
          width="240"
        >
          <template slot-scope="{ row }">
            <!-- 后面使用自定义标签 -->
            <el-tag
              :type="row.httpMethod | requestMethodFilter"
            >
              {{ row.httpMethod }}
            </el-tag>
            <el-tag
              effect="dark"
              :type="row.executionDuration | requestDurationFilter"
            >
              {{ row.executionDuration }}S
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column
          :label="$t('AbpAuditLogging[\'ApplicationName\']')"
          prop="applicationName"
          align="center"
        >
          <template slot-scope="{ row }">
            <span>{{ row.applicationName }}</span>
          </template>
        </el-table-column>
        <el-table-column
          :label="$t('AbpAuditLogging[\'ClientIpAddress\']')"
          prop="clientIpAddress"
          align="center"
        >
          <template slot-scope="{ row }">
            <span>{{ row.clientIpAddress }}</span>
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
  </div>
</template>

<script>
import {
  getAuditLogs
} from '@/api/auditlogging/auditlog'
import { pickerRangeWithHotKey } from '@/utils/picker'
import Pagination from '@/components/Pagination'
import baseListQuery, { httpCode } from '@/utils/abp'
export default {
  name: 'AuditLog',
  components: { Pagination },
  filters: {
    requestDurationFilter(duration) {
      let type = 'success'
      if (duration > 2 * 1000) {
        type = 'warning'
      } else if (duration > 5 * 1000) {
        type = 'error'
      }
      return type
    },
    requestMethodFilter(method) {
      let type = 'success'
      switch (method.toUpperCase()) {
        case 'GET':
          type = 'success'
          break
        case 'PUT':
          type = 'warning'
          break
        case 'POST':
          type = ''
          break
        case 'DELETE':
          type = 'danger'
          break
        default:
          type = 'Info'
      }
      return type
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      queryForm: {
        startTime: undefined,
        endTime: undefined,
        httpMethod: undefined,
        url: undefined,
        userName: undefined,
        applicationName: undefined,
        // correlationId: '',
        // maxExecutionDuration: '',
        hasException: false,
        httpStatusCode: httpCode['200'],
        includeDetails: false
      },
      listQuery: baseListQuery,
      pickerOptions: pickerRangeWithHotKey,
      value1: [new Date(2000, 10, 10, 10, 10), new Date(2000, 10, 11, 10, 10)],
      value2: ''
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getAuditLogs(Object.assign(this.queryForm, this.listQuery)).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    }
  }
}
</script>
