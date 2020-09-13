import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getAuditLogs(query) {
  return request({
    url: '/api/audit-logging/audit-logs',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getAuditLog(id) {
  return request({
    url: `/api/audit-logging/audit-logs/${id}`,
    method: 'get'
  })
}

export function deleteAuditLog(id) {
  return request({
    url: `/api/audit-logging/audit-logs/${id}`,
    method: 'delete'
  })
}

export function deleteManyAuditLog(ids) {
  return request({
    url: `/api/audit-logging/audit-logs/delete-many`,
    method: 'delete',
    data: ids
  })
}
