import request from '@/utils/request'

export function getSettingValues() {
  return request({
    url: '/api/setting-ui',
    method: 'get'
  })
}

export function setSettingValues(values) {
  return request({
    url: '/api/setting-ui/set-setting-values',
    method: 'put',
    data: values
  })
}

export function resetSettingValues(values) {
  return request({
    url: '/api/setting-ui/reset-setting-values',
    method: 'put',
    data: values
  })
}
