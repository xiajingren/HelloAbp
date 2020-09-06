import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getOrganizationsAllWithDetails(query) {
  return request({
    url: '/api/identity/organizations/all/details',
    method: 'get',
    params: query
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
export function getOrganizationsRoot() {
  return request({
    url: '/api/identity/organizations/root',
    method: 'get'
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

export function getOrgUsers(query) {
  return request({
    url: '/api/identity/organizations/users',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getOrgRoles(query) {
  return request({
    url: '/api/identity/organizations/roles',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createOrganization(payload) {
  return request({
    url: '/api/identity/organizations',
    method: 'post',
    data: payload
  })
}

export function updateOrganization(id, payload) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteOrganization(id) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'delete'
  })
}
