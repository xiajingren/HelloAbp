<template>
  <div class="audit-log-container">
    <el-dialog
      title="日志详情"
      :visible.sync="dialogVisible"
    >
      <table class="logInfo">
        <tbody>
          <tr>
            <th>Code</th>
            <td>{{ logData.httpStatusCode }}</td>
          </tr>
          <tr>
            <th>Method</th>
            <td>{{ logData.httpMethod }}</td>
          </tr>
          <tr>
            <th>Url</th>
            <td>{{ logData.url }}</td>
          </tr>
          <tr>
            <th>ClientIP</th>
            <td>{{ logData.clientIpAddress }}</td>
          </tr>
          <tr>
            <th>ClientName</th>
            <td>{{ logData.clientName }}</td>
          </tr>
          <tr>
            <th>UserName</th>
            <td>{{ logData.userName }}</td>
          </tr>
          <tr>
            <th>请求时间</th>
            <td>{{ logData.executionTime | moment }}</td>
          </tr>
          <tr>
            <th>执行耗时</th>
            <td>{{ logData.executionDuration }}ms</td>
          </tr>
          <tr>
            <th>浏览器信息</th>
            <td>{{ logData.browserInfo }}</td>
          </tr>
          <tr>
            <th>异常信息</th>
            <td>{{ logData.exceptions }}</td>
          </tr>
          <tr>
            <th>Commants</th>
            <td>{{ logData.comments }}</td>
          </tr>
          <tr>
            <th>扩展信息</th>
            <td><pre>{{ logData.extraProperties }}</pre> </td>
          </tr>
        </tbody>
      </table>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">
          {{ $t("AbpAuditLogging['Close']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getAuditLog
} from '@/api/auditlogging/auditlog'
export default {
  name: 'AuditLogDetails',
  data() {
    return {
      dialogVisible: false,
      logData: {}
    }
  },
  methods: {
    getDetails() {
      getAuditLog(this.logData.id).then(response => {
        this.logData = response
      })
    },
    createLogInfo(row) {
      this.dialogVisible = true
      this.logData = row
    }
  }
}
</script>

<style lang="scss" scoped>
.audit-log-container{
  .logInfo{
    border-collapse: collapse;
    border-spacing: 2px;
    tr{
      border: 1px solid #f0f0f0;
      th{
        background-color: #fafafa;
        width: 120px;
      }
      th,td{
        text-align: left;
        text-overflow: ellipsis;
        vertical-align: middle;
        box-sizing: border-box;
        border-right: #f0f0f0;
        height: inherit;
        padding: 8px 16px;
      }
    }
  }
}
</style>
