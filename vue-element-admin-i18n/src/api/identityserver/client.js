import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getClients(query) {
  return request({
    url: '/api/identity-server/clients',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function createClient(payload) {
  return request({
    url: '/api/identity-server/clients',
    method: 'post',
    data: payload
  })
}

export function getClientById(id) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'get'
  })
}

export function updateClient(payload) {
  return request({
    url: `/api/identity-server/clients/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteClient(id) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'delete'
  })
}

export function getClientsAll() {
  return request({
    url: '/api/identity-server/clients/all',
    method: 'get'
  })
}
