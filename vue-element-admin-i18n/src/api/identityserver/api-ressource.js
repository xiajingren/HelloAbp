import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getApiResources(query) {
  return request({
    url: '/api/identity-server/api-resources',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createApiResource(payload) {
  return request({
    url: '/api/identity-server/api-resources',
    method: 'post',
    data: payload
  })
}

export function getApiResourceById(id) {
  return request({
    url: `/api/identity-server/api-resources/${id}`,
    method: 'get'
  })
}

export function updateApiResource(payload) {
  return request({
    url: `/api/identity-server/api-resources/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteApiResource(id) {
  return request({
    url: `/api/identity-server/api-resources/${id}`,
    method: 'delete'
  })
}
