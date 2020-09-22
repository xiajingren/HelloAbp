import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getDatationaryList(payload) {
  return request({
    url: '/api/data-dictionary/dictionary',
    method: 'get',
    params: transformAbpListQuery(payload)
  })
}

export function getDatationary(id) {
  return request({
    url: `/api/data-dictionary/dictionary/${id}`,
    method: 'get'
  })
}
export function deleteDatationary(id) {
  return request({
    url: `/api/data-dictionary/dictionary/${id}`,
    method: 'delete'
  })
}

export function deleteDatationaries(ids) {
  let urlIds = '?'
  if (ids && ids.length) {
    for (let index = 0; index < ids.length; index++) {
      urlIds += `ids=${ids[index]}&`
    }
    urlIds = urlIds.substring(0, urlIds.length - 1)
  }
  return request({
    url: `/api/data-dictionary/dictionary${urlIds}`,
    method: 'delete'
  })
}

export function updateDatationary(id, payload) {
  return request({
    url: `/api/data-dictionary/dictionary/${id}`,
    method: 'put',
    data: payload
  })
}

export function createDatadictionary(payload) {
  return request({
    url: '/api/data-dictionary/dictionary',
    method: 'post',
    data: payload
  })
}

// details
export function getDatationaryDetailList(payload) {
  return request({
    url: '/api/data-dictionary/dictionary-detail',
    method: 'get',
    params: transformAbpListQuery(payload)
  })
}

export function getDatationaryDetail(id) {
  return request({
    url: `/api/data-dictionary/dictionary-detail/${id}`,
    method: 'get'
  })
}
export function deleteDatationaryDetail(id) {
  return request({
    url: `/api/data-dictionary/dictionary-detail/${id}`,
    method: 'delete'
  })
}

export function deleteDatationariesDetail(ids) {
  let urlIds = '?'
  if (ids && ids.length) {
    for (let index = 0; index < ids.length; index++) {
      urlIds += `ids=${ids[index]}&`
    }
    urlIds = urlIds.substring(0, urlIds.length - 1)
  }
  return request({
    url: `/api/data-dictionary/dictionary-detail${urlIds}`,
    method: 'delete'
  })
}

export function updateDatationaryDetail(id, payload) {
  return request({
    url: `/api/data-dictionary/dictionary-detail/${id}`,
    method: 'put',
    data: payload
  })
}

export function createDatadictionaryDetail(payload) {
  return request({
    url: '/api/data-dictionary/dictionary-detail',
    method: 'post',
    data: payload
  })
}
