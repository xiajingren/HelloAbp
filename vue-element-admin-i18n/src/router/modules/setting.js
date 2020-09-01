import Layout from '@/layout'

const settingRouter = {
  path: '/setting',
  component: Layout,
  redirect: 'noRedirect',
  alwaysShow: true,
  name: 'Setting',
  meta: {
    title: 'AbpSettingManagement["Settings"]',
    icon: 'el-icon-setting',
    policy: ''
  },
  children: [
    {
      path: 'businessRule',
      component: () => import('@/views/settings/setting'),
      name: 'BusinessRule',
      meta: { title: '业务规则设置' }
    }
  ]
}

export default settingRouter
