<template>
  <div class="app-container">

    <el-tabs tab-position="left">
      <el-tab-pane
        v-for="(group,sIndex) in settingData"
        :key="group.groupName"
        :label="group.groupDisplayName"
      >
        <el-tabs tab-position="top">
          <el-tab-pane
            v-for="card in group.settingInfos"
            :key="card[0].properties.Group2"
            :label="$t(`SettingUi.${card[0].properties.Group2}`)"
          >
            <el-card
              class="box-card"
            >
              <div
                slot="header"
                class="clearfix"
              >
                <span>{{ $t(`SettingUi.${card[0].properties.Group2}`) }}</span>
              </div>
              <div class="text item">
                <el-form
                  :ref="card[0].properties.Group2"
                  label-position="top"
                >
                  <el-form-item
                    v-for="info in card"
                    :key="info.name"
                    :label="info.displayName"
                  >
                    <div v-if="info.properties.Type==='number' || info.properties.Type==='text'">
                      <label :for="info.name">{{ info.description }}</label>
                      <el-input
                        :id="info.name"
                        v-model="info.value"
                        :type="info.properties.Type"
                        :name="info.formName"
                      />
                    </div>
                    <div v-if="info.properties.Type==='checkbox'">
                      <el-checkbox
                        :id="info.name"
                        v-model="info.value"
                        :name="info.formName"
                        :checked="info.value==='true' || info.value==='True'"
                      >{{ info.description }}</el-checkbox>
                    </div>
                    <div v-if="info.properties.Type==='select'">
                      <label :for="info.name">{{ info.description }}</label>
                      <el-select
                        :id="info.name"
                        v-model="info.value"
                        :name="info.formName"
                      >
                        <el-option
                          v-for="item in info.properties.Options.split('|')"
                          :key="item"
                          :label="item"
                          :value="item"
                        />
                      </el-select>
                    </div>
                  </el-form-item>
                  <el-button
                    type="primary"
                    @click="updateSettingValues(card[0].properties.Group2,sIndex)"
                  >{{ $t('SettingUi.Save') }}</el-button>
                </el-form>
              </div>
            </el-card>
          </el-tab-pane>
        </el-tabs>
      </el-tab-pane>
    </el-tabs>

  </div>
</template>

<script>
import { getSettingValues, setSettingValues, resetSettingValues } from '@/api/settings/setting'
export default {
  name: 'Setting',
  data() {
    return {
      settingData: [],
      formRefs: []
    }
  },
  created() {
    this.getGroupSettingDefinitions()
  },
  methods: {
    getGroupSettingDefinitions() {
      getSettingValues().then(setting => {
        for (const s of setting) {
          const settingInfo = []
          for (const info of s.settingInfos) {
            const group2 = info.properties.Group2
            const formRefsKey = s.groupName + '.' + group2
            settingInfo[group2] = settingInfo[group2] || []
            info.formName = 'Setting_' + info.name.replace(/\./g, '_')
            settingInfo[group2].push(info)
            if (this.formRefs.indexOf(formRefsKey) <= 0) {
              this.formRefs.push(formRefsKey)
            }
          }
          console.log(...settingInfo)
          this.settingData.push({
            groupName: s.groupName,
            groupDisplayName: s.groupDisplayName,
            settingInfos: { ...settingInfo }
          })
        }
      })
    },
    updateSettingValues(formName, index) {
      const obj = {}
      for (const from of this.settingData[index].settingInfos[formName]) {
        const { formName, value } = from
        obj[formName] = value
      }
      setSettingValues(obj).then(() => {
        this.$notify({
          title: this.$i18n.t("HelloAbp['Success']"),
          message: this.$i18n.t("SettingUi['SuccessfullySaved']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    repalceSettingValues() {
      // TODO:
      return resetSettingValues()
    }
  }
}
</script>

<style lang="scss" scoped>
.app-container {
}
</style>
