import store from '@/store'
import router, { resetRouter } from '@/router'

const baseListQuery = {
  page: 1,
  limit: 20,
  sort: undefined,
  filter: undefined
}

export const httpCode = {
  100: 100,
  101: 101,
  102: 102,
  103: 103,
  200: 200,
  201: 201,
  202: 202,
  203: 203,
  204: 204,
  205: 205,
  206: 206,
  207: 207,
  208: 208,
  226: 226,
  300: 300,
  301: 301,
  302: 302,
  303: 303,
  304: 304,
  305: 305,
  306: 306,
  307: 307,
  308: 308,
  400: 400,
  401: 401,
  402: 402,
  403: 403,
  404: 404,
  405: 405,
  406: 406,
  407: 407,
  408: 408,
  409: 409,
  410: 410,
  411: 411,
  412: 412,
  413: 413,
  414: 414,
  415: 415,
  416: 416,
  417: 417,
  421: 421,
  422: 422,
  423: 423,
  424: 424,
  426: 426,
  428: 428,
  429: 429,
  431: 431,
  451: 451,
  500: 500,
  501: 501,
  502: 502,
  503: 503,
  504: 504,
  505: 505,
  506: 506,
  507: 507,
  508: 508,
  510: 510,
  511: 511
}

export function transformAbpListQuery(query) {
  query.filter = query.filter === '' ? undefined : query.filter

  const abpListQuery = {
    maxResultCount: query.limit,
    skipCount: (query.page - 1) * query.limit,
    sorting:
      query.sort && query.sort.endsWith('ending')
        ? query.sort.replace('ending', '')
        : query.sort,
    ...query
  }

  delete abpListQuery.page
  delete abpListQuery.limit
  delete abpListQuery.sort

  return abpListQuery
}

function shouldFetchAppConfig(providerKey, providerName) {
  const currentUser = store.getters.abpConfig.currentUser

  if (providerName === 'R') { return currentUser.roles.some(role => role === providerKey) }

  if (providerName === 'U') return currentUser.id === providerKey

  return false
}

export function fetchAppConfig(providerKey, providerName) {
  if (shouldFetchAppConfig(providerKey, providerName)) {
    store.dispatch('app/applicationConfiguration').then(abpConfig => {
      resetRouter()

      store.dispatch('user/setRoles', abpConfig.currentUser.roles)

      const grantedPolicies = abpConfig.auth.grantedPolicies

      // generate accessible routes map based on grantedPolicies
      store
        .dispatch('permission/generateRoutes', grantedPolicies)
        .then(accessRoutes => {
          // dynamically add accessible routes
          router.addRoutes(accessRoutes)
        })

      // reset visited views and cached views
      // store.dispatch("tagsView/delAllViews", null, { root: true });
    })
  }
}

export function checkPermission(policy) {
  const abpConfig = store.getters.abpConfig
  if (abpConfig.auth.grantedPolicies[policy]) {
    return true
  } else {
    return false
  }
}

export default baseListQuery
