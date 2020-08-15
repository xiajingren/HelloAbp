/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity',
  component: Layout,
  redirect: 'noRedirect',
  name: 'Identity',
  meta: {
    title: 'AbpIdentity["Menu:IdentityManagement"]',
    icon: 'user'
  },
  children: [
    {
      path: 'roles',
      component: () => import('@/views/identity/roles'),
      name: 'Roles',
      meta: { title: 'AbpIdentity["Permission:RoleManagement"]', policy: 'AbpIdentity.Roles' }
    },
    {
      path: 'users',
      component: () => import('@/views/identity/users'),
      name: 'Users',
      meta: { title: 'AbpIdentity["Permission:UserManagement"]', policy: 'AbpIdentity.Users' }
    }
  ]
}
export default identityRouter
