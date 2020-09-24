import Layout from '@/layout'

const dictionaryRouter = {
  path: '/dictionaries',
  component: Layout,
  children: [
    {
      path: 'index',
      component: () => import('@/views/data-dictionary/index'),
      name: 'DataDictionary',
      meta: {
        title: 'AbpDataDictionary["DataDictionary"]',
        policy: 'AbpDataDictionary.Default',
        icon: 'el-icon-reading'
      }
    }
  ]
}

export default dictionaryRouter
