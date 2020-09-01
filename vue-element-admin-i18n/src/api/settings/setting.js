import request from '@/utils/request'

export function getSettingValues() {
  return request({
    url: '/api/settingUi',
    method: 'get'
  })
}

export function setSettingValues(values) {
  return request({
    url: '/api/settingUi/setSettingValues',
    method: 'put',
    data: values
  })
}

export function resetSettingValues(values) {
  return request({
    url: '/api/settingUi/resetSettingValues',
    method: 'put',
    data: values
  })
}
