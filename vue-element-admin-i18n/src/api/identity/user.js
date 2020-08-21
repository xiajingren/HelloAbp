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

export function updateUser(payload) {
  return request({
    url: `/api/identity/users/${payload.id}`,
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
