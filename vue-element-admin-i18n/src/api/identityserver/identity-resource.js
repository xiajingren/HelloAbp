import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getIdentityResources(query) {
  return request({
    url: '/api/identity-server/identity-resources',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createIdentityResource(payload) {
  return request({
    url: '/api/identity-server/identity-resources',
    method: 'post',
    data: payload
  })
}

export function getIdentityResourceById(id) {
  return request({
    url: `/api/identity-server/identity-resources/${id}`,
    method: 'get'
  })
}

export function updateIdentityResource(payload) {
  return request({
    url: `/api/identity-server/identity-resources/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteIdentityResource(id) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'delete'
  })
}
