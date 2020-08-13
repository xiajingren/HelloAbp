/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity',
  component: Layout,
  redirect: 'noRedirect',
  name: 'Identity',
  meta: {
    title: 'identity',
    icon: 'user'
  },
  children: [
    {
      path: 'roles',
      component: () => import('@/views/identity/roles'),
      name: 'Roles',
      meta: { title: 'roles', policy: 'AbpIdentity.Roles' }
    },
    {
      path: 'users',
      component: () => import('@/views/identity/users'),
      name: 'Users',
      meta: { title: 'users', policy: 'AbpIdentity.Users' }
    }
  ]
}
export default identityRouter
