import request from '@/utils/request'
import qs from 'querystring'

export function login(data) {
  return request({
    url: '/connect/token',
    method: 'post',
    headers: { 'content-type': 'application/x-www-form-urlencoded' },
    data: qs.stringify(data)
  })
}

export function getInfo() {
  return request({
    url: '/api/identity/my-profile',
    method: 'get'
  })
}

export function logout() {
  return request({
    url: '/api/account/logout',
    method: 'get'
  })
}

export function register(data) {
  return request({
    url: '/api/account/register',
    method: 'post',
    data: data
  })
}

export function setUserInfo(data) {
  return request({
    url: '/api/identity/my-profile',
    method: 'put',
    data: data
  })
}

export function changePassword(data) {
  return request({
    url: '/api/identity/my-profile/change-password',
    method: 'post',
    data: data
  })
}
