/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity',
  component: Layout,
  redirect: 'noRedirect',
  alwaysShow: true,
  name: 'Identity',
  meta: {
    title: 'AbpIdentity["Menu:IdentityManagement"]',
    icon: 'user',
    policy: ''
  },
  children: [
    {
      path: 'organizations',
      component: () => import('@/views/identity/organizations'),
      name: 'Organizations',
      meta: { title: 'AbpIdentity["OrganitaionUnits"]', policy: 'AbpIdentity.OrganitaionUnits' }
    },
    {
      path: 'roles',
      component: () => import('@/views/identity/roles'),
      name: 'Roles',
      meta: { title: 'AbpIdentity["Roles"]', policy: 'AbpIdentity.Roles' }
    },
    {
      path: 'users',
      component: () => import('@/views/identity/users'),
      name: 'Users',
      meta: { title: 'AbpIdentity["Users"]', policy: 'AbpIdentity.Users' }
    },
    {
      path: 'claim-types',
      component: () => import('@/views/identity/claim-types'),
      name: 'ClaimTypes',
      meta: { title: 'AbpIdentity["ClaimTypes"]', policy: 'AbpIdentity.ClaimTypes' }
    }
  ]
}
export default identityRouter
