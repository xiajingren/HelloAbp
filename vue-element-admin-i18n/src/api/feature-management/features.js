import request from '@/utils/request'

export function getFeatures(query) {
  return request({
    url: '/api/feature-management/features',
    method: 'get',
    params: query
  })
}

export function updateFeatures(query, payload) {
  return request({
    url: '/api/feature-management/features',
    method: 'put',
    params: query,
    data: payload
  })
}
