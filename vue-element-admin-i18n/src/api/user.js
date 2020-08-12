import request from '@/utils/request'
import qs from 'querystring'

export function login(data) {
  return request({
    baseURL: 'https://localhost:44364',
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
    baseURL: 'https://localhost:44364',
    url: '/api/account/logout',
    method: 'get'
  })
}
