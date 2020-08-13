/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const tenantRouter = {
  path: '/tenant',
  component: Layout,
  redirect: '/tenant/tenants',
  alwaysShow: true,
  name: 'Tenant',
  meta: {
    title: 'tenant',
    icon: 'tree'
  },
  children: [
    {
      path: 'tenants',
      component: () => import('@/views/tenant/tenants'),
      name: 'Tenants',
      meta: { title: 'tenants', policy: 'AbpTenantManagement.Tenants' }
    }
  ]
}
export default tenantRouter
