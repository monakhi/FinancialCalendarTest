<template>
  <div class="home">
    <FinancialCalendar />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import FinancialCalendar from '@/components/FinancialCalendar.vue'

export default defineComponent({
  name: 'HomeView',
  components: {
    FinancialCalendar
  }
})
</script>
