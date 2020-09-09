import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getAuditLogs(query) {
  return request({
    url: '/api/autit/logs',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function deleteAuditLog(id) {
  return request({
    url: '',
    method: 'delete'
  })
}
