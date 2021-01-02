/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity-server',
  component: Layout,
  redirect: 'noRedirect',
  alwaysShow: true,
  name: 'AbpIdentityServer',
  meta: {
    title: 'AbpIdentityServer["Menu:IdentityServerManagement"]',
    icon: 'user',
    policy: ''
  },
  children: [
    {
      path: 'clients',
      component: () => import('@/views/identityserver/clients'),
      name: 'Clients',
      meta: { title: 'AbpIdentityServer["Menu:ClientManagement"]', policy: 'AbpIdentityServer.Clients' }
    },
    {
      path: 'identity-resources',
      component: () => import('@/views/identityserver/identity-resource'),
      name: 'IdentityResource',
      meta: { title: 'AbpIdentityServer["Menu:IdentityResourceManagement"]', policy: 'AbpIdentityServer.IdentityResources' }
    },
    {
      path: 'api-resources',
      component: () => import('@/views/identityserver/api-resource'),
      name: 'ApiResource',
      meta: { title: 'AbpIdentityServer["Menu:ApiResourceManagement"]', policy: 'AbpIdentityServer.ApiResources' }
    }
  ]
}
export default identityRouter
