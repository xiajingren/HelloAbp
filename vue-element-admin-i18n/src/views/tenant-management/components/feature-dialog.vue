<template>
  <el-dialog
    :title="$t('AbpFeatureManagement[\'Features\']')"
    :visible.sync="dialogFormVisible"
  >
    <el-form
      ref="dataForm"
      :rules="rules"
      :model="temp"
      label-position="right"
      label-width="120px"
    >
      <el-form-item
        v-for="feature in features"
        :key="feature.name"
        :label="feature.displayName"
      >
        <el-checkbox
          v-if="feature.valueType.name === 'ToggleStringValueType'"
          v-model="temp[feature.name]"
        />
        <el-input
          v-else-if="feature.valueType.name === 'FreeTextStringValueType'"
          v-model="temp[feature.name]"
        />
      </el-form-item>

      <aside v-if="!features || features.length == 0">
        {{ $t("AbpFeatureManagement['NoFeatureFoundMessage']") }}
      </aside>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogFormVisible = false">
        {{ $t("AbpFeatureManagement['Cancel']") }}
      </el-button>
      <el-button
        v-if="features && features.length != 0"
        type="primary"
        @click="updateData()"
      >
        {{ $t("AbpFeatureManagement['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getFeatures, updateFeatures } from '@/api/feature-management/features'

export default {
  name: 'FeatureDialog',
  props: {
    providerName: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      features: '',
      temp: {},
      dialogFormVisible: false,
      rules: {},
      featuresQuery: { providerKey: '', providerName: '' }
    }
  },
  created() {
    this.featuresQuery.providerName = this.providerName
  },
  methods: {
    resetTemp() {
      this.temp = {}
    },
    handleUpdate(row) {
      this.resetTemp()
      this.featuresQuery.providerKey = row.id
      this.dialogFormVisible = true

      getFeatures(this.featuresQuery).then(response => {
        this.features = response.features

        this.features.map(feature => {
          if (feature.valueType.name === 'ToggleStringValueType') {
            this.$set(
              this.temp,
              feature.name,
              feature.value === 'true'
            )
          } else if (feature.valueType.name === 'FreeTextStringValueType') { this.$set(this.temp, feature.name, feature.value) }
        })
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          const tempData = { features: [] }
          this.features.map(feature => {
            if (feature.valueType.name === 'ToggleStringValueType') {
              tempData.features.push({
                name: feature.name,
                value: !!this.temp[feature.name]
              })
            } else if (feature.valueType.name === 'FreeTextStringValueType') {
              tempData.features.push({
                name: feature.name,
                value: this.temp[feature.name]
              })
            }
          })

          updateFeatures(this.featuresQuery, tempData).then(() => {
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("HelloAbp['Success']"),
              message: this.$i18n.t("HelloAbp['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    }
  }
}
</script>
