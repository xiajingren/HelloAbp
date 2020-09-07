import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getUsers(query) {
  return request({
    url: '/api/identity/users',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getUserById(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'get'
  })
}

export function createUser(payload) {
  return request({
    url: '/api/identity/users',
    method: 'post',
    data: payload
  })
}

export function createUserToOrg(payload) {
  return request({
    url: '/api/identity/users/create-to-organizations',
    method: 'post',
    data: payload
  })
}

export function updateUser(payload) {
  return request({
    url: `/api/identity/users/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function updateUserToOrg(payload) {
  return request({
    url: `/api/identity/users/${payload.id}/update-to-organizations`,
    method: 'put',
    data: payload
  })
}

export function deleteUser(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  })
}

export function getRolesByUserId(id) {
  return request({
    url: `/api/identity/users/${id}/roles`,
    method: 'get'
  })
}

export function getAssignableRoles() {
  return request({
    url: '/api/identity/users/assignable-roles',
    method: 'get'
  })
}

export function getOrganizationsByUserId(id, includeDetails = false) {
  return request({
    url: `/api/identity/users/${id}/organizations`,
    method: 'get',
    params: includeDetails
  })
}

/**
 * 添加成员到组织单元中
 * @param {string} id
 * @param {Array} ouId
 */
export function addToOrganization(id, ouIds) {
  return request({
    url: `/api/identity/users/${id}/add-to-organizations`,
    method: 'post',
    data: ouIds
  })
}
