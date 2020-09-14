<template>
  <div class="audit-log-container">
    <el-dialog
      :title="logData.url+
        '-'+
        $t('AbpAuditLogging[\'Detail\']')"
      :visible.sync="dialogVisible"
    >
      <el-tabs type="border-card">
        <el-tab-pane
          :label="$t('AbpAuditLogging[\'RequsetInfo\']')"
        >
          <table class="logInfo">
            <tbody>
              <tr>
                <th>{{ $t("AbpAuditLogging['HttpStatusCode']") }}</th>
                <td>{{ logData.httpStatusCode }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['HttpMethod']") }}</th>
                <td>{{ logData.httpMethod }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['Url']") }}</th>
                <td>{{ logData.url }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ClientIpAddress']") }}</th>
                <td>{{ logData.clientIpAddress }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ClientName']") }}</th>
                <td>{{ logData.clientName }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['UserName']") }}</th>
                <td>{{ logData.userName }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExecutionTime']") }}</th>
                <td>{{ logData.executionTime | moment }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExecutionDuration']") }}</th>
                <td>{{ logData.executionDuration }}ms</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['BrowserInfo']") }}</th>
                <td>{{ logData.browserInfo }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['Exceptions']") }}</th>
                <td>{{ logData.exceptions }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['Comments']") }}</th>
                <td>{{ logData.comments }}</td>
              </tr>
              <tr>
                <th>{{ $t("AbpAuditLogging['ExtraProperties']") }}</th>
                <td><pre>{{ logData.extraProperties }}</pre> </td>
              </tr>
            </tbody>
          </table>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('AbpAuditLogging[\'Actions\']')"
        >
          <el-collapse
            v-if="logData.actions && logData.actions.length>0"
          >
            <el-collapse-item
              v-for="action in logData.actions"
              :key="action.id"
              :title="action.serviceName"
              :name="action.serviceName"
            >
              <table class="logInfo">
                <tbody>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['MethodName']") }}</th>
                    <td>{{ action.methodName }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExecutionTime']") }}</th>
                    <td>{{ action.executionTime | moment }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExecutionDuration']") }}</th>
                    <td>{{ action.executionDuration }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['Parameters']") }}</th>
                    <td>
                      <span>{{ action.parameters }}</span>
                      <el-button
                        type="primary"
                        round
                        size="small"
                        icon="el-icon-document"
                        @click="handleCopyParameters(action.parameters,$event)"
                      >
                        {{ $t("table.copy") }}
                      </el-button>
                    </td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExtraProperties']") }}</th>
                    <td>{{ action.extraProperties }}</td>
                  </tr>
                </tbody>
              </table>
            </el-collapse-item>
          </el-collapse>
          <p v-else>
            {{ $t('table.empty') }}
          </p>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('AbpAuditLogging[\'EntityChanges\']')"
        >
          <el-collapse
            v-if="logData.entityChanges && logData.entityChanges.length>0"
          >
            <el-collapse-item
              v-for="entity in logData.entityChanges"
              :key="entity.id"
              :title="entity.entityTypeFullName"
              :name="entity.entityTypeFullName"
            >
              <table class="logInfo">
                <tbody>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['EntityTypeFullName']") }}</th>
                    <td>{{ action.entityTypeFullName }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ChangeType']") }}</th>
                    <td>{{ action.changeType }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ChangeTime']") }}</th>
                    <td>{{ action.changeTime | moment }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['EntityId']") }}</th>
                    <td>{{ action.entityId }}</td>
                  </tr>
                  <tr>
                    <th>{{ $t("AbpAuditLogging['ExtraProperties']") }}</th>
                    <td>{{ action.extraProperties }}</td>
                  </tr>
                </tbody>
              </table>
            </el-collapse-item>
          </el-collapse>
          <p v-else>
            {{ $t('table.empty') }}
          </p>
        </el-tab-pane>
      </el-tabs>

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
import clip from '@/utils/clipboard'
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
      this.getDetails()
    },
    handleCopyParameters(text, event) {
      clip(text, event)
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
