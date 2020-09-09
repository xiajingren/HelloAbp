import Layout from '@/layout'

const auditlogRouter = {
  path: '/auditlogs',
  component: Layout,
  children: [
    {
      path: 'auditlog',
      component: () => import('@/views/auditlogging/index'),
      name: 'AuditLog',
      meta: {
        title: 'AbpAuditLogging["AuditLogging"]',
        policy: 'AbpAuditLogging.Default',
        icon: 'el-icon-document'
      }
    }
  ]
}

export default auditlogRouter
