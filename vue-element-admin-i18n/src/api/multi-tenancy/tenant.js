import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTenants(query) {
  return request({
    url: '/api/multi-tenancy/tenants',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getTenantById(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'get'
  })
}

export function createTenant(payload) {
  return request({
    url: '/api/multi-tenancy/tenants',
    method: 'post',
    data: payload
  })
}

export function updateTenant(payload) {
  return request({
    url: `/api/multi-tenancy/tenants/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTenant(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'delete'
  })
}

export function getDefaultConnectionString(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'get'
  })
}

export function updateDefaultConnectionString(id, query) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'put',
    params: query
  })
}

export function deleteDefaultConnectionString(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}/default-connection-string`,
    method: 'delete'
  })
}
