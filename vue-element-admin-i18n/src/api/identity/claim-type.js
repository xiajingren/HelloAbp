import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getClaimTypes(query) {
  return request({
    url: '/api/identity/claim-types',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createClaimType(payload) {
  return request({
    url: '/api/identity/claim-types',
    method: 'post',
    data: payload
  })
}

export function getClaimTypeById(id) {
  return request({
    url: `/api/identity/claim-types/${id}`,
    method: 'get'
  })
}

export function updateClaimType(payload) {
  return request({
    url: `/api/identity/claim-types/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteClaimType(id) {
  return request({
    url: `/api/identity/claim-types/${id}`,
    method: 'delete'
  })
}

export function getClaimTypesAll() {
  return request({
    url: '/api/identity/claim-types/all',
    method: 'get'
  })
}
