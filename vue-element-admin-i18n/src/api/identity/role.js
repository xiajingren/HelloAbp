import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getRoles(query) {
  return request({
    url: '/api/identity/roles',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getRoleById(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'get'
  })
}

export function createRole(payload) {
  return request({
    url: '/api/identity/roles',
    method: 'post',
    data: payload
  })
}

export function createRoleToOrg(payload) {
  return request({
    url: '/api/identity/roles/create-to-organization',
    method: 'post',
    data: payload
  })
}

export function updateRole(payload) {
  return request({
    url: `/api/identity/roles/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}
