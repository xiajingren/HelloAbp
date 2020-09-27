<template>
  <el-dropdown trigger="click" @command="handleSetSize">
    <div>
      <svg-icon class-name="size-icon" icon-class="size" />
    </div>
    <el-dropdown-menu slot="dropdown">
      <el-dropdown-item v-for="item of sizeOptions" :key="item.value" :disabled="size===item.value" :command="item.value">
        {{
          item.label }}
      </el-dropdown-item>
    </el-dropdown-menu>
  </el-dropdown>
</template>

<script>
export default {
  data() {
    return {
      sizeOptions: [
        { label: this.$t('switchSize.default'), value: 'default' },
        { label: this.$t('switchSize.medium'), value: 'medium' },
        { label: this.$t('switchSize.small'), value: 'small' },
        { label: this.$t('switchSize.mini'), value: 'mini' }
      ]
    }
  },
  computed: {
    size() {
      return this.$store.getters.size
    },
    language() {
      return this.$store.getters.abpConfig.localization.languages
    }
  },
  watch: {
    language(val) {
      console.log(val)
      this.sizeOptions = [
        { label: this.$t('switchSize.default'), value: 'default' },
        { label: this.$t('switchSize.medium'), value: 'medium' },
        { label: this.$t('switchSize.small'), value: 'small' },
        { label: this.$t('switchSize.mini'), value: 'mini' }
      ]
    }
  },
  methods: {
    handleSetSize(size) {
      this.$ELEMENT.size = size
      this.$store.dispatch('app/setSize', size)
      this.refreshView()
      this.$message({
        message: this.$t('switchSize.switchSizeSuccess'),
        type: 'success'
      })
    },
    refreshView() {
      // In order to make the cached page re-rendered
      this.$store.dispatch('tagsView/delAllCachedViews', this.$route)

      const { fullPath } = this.$route

      this.$nextTick(() => {
        this.$router.replace({
          path: '/redirect' + fullPath
        })
      })
    }
  }

}
</script>
