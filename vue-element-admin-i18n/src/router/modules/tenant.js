/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const tenantRouter = {
  path: '/tenant',
  component: Layout,
  redirect: '/tenant/tenants',
  alwaysShow: true,
  name: 'Tenant',
  meta: {
    title: 'AbpTenantManagement["Menu:TenantManagement"]',
    icon: 'tree',
    policy: ''
  },
  children: [
    {
      path: 'tenants',
      component: () => import('@/views/tenant-management/tenants'),
      name: 'Tenants',
      meta: { title: 'AbpTenantManagement["Tenants"]', policy: 'AbpTenantManagement.Tenants' }
    }
  ]
}
export default tenantRouter
