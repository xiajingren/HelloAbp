import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getOrganizationsAllWithDetails() {
  return request({
    url: '/api/identity/organizations/all/details',
    method: 'get'
  })
}

/**
 * 获取组织机构
 * Example: ?filter&sorting&skipCount=0&maxResultCount=20
 * @param {object} query
 */
export function getOrganizationsWithDetails(query) {
  return request({
    url: '/api/identity/organizations/details',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

/**
 * 后期考虑提交PR给abp,没有获取根节点的方法
 * @param {object} query
 */
export function getOrganizationsRoot(query) {
  return request({
    url: '/api/identity/organizations/root',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getOrganizationSingleWithDetails(id) {
  return request({
    url: `/api/identity/organizations/${id}/details`,
    method: 'get'
  })
}

export function getOrganizationSingle(id) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'get'
  })
}

export function getOrganizationsAll() {
  return request({
    url: '/api/identity/organizations/all',
    method: 'get'
  })
}

export function getOrganizations(query) {
  return request({
    url: '/api/identity/organizations',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getOrganizationChildren(pid) {
  return request({
    url: `/api/identity/organizations/children/${pid}`,
    method: 'get'
  })
}

export function createOrganization(payload) {
  return request({
    url: '/api/identity/organizations',
    method: 'post',
    data: payload
  })
}

export function updateOrganization(payload) {
  return request({
    url: `/api/identity/organizations/${payload.id}`,
    method: 'post',
    data: payload
  })
}

export function deleteOrganization(id) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'delete'
  })
}
