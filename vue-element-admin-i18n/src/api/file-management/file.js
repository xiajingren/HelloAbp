import request from '@/utils/request'

export function createFile(data) {
  return request({
    url: '/api/file-management/files/upload',
    method: 'post',
    data: data
  })
}

export function getFileByName(name) {
  return request({
    url: `/api/file-management/files/${name}`,
    method: 'get',
    responseType: 'arraybuffer'
  })
}
