<template>
  <div class="container">
    <h1>Financial Calendars</h1>
    <p> Please enter Start Date</p>
    <input type="date" v-model="startDate" @change="resetPageNo"/>
    <div class="mt-2">
        <button type="button" class="btn btn-success" @click="getData">Submit</button>
        <button type="button" class="btn btn-info ml-1" @click="incrementPageNo">Next</button>
    </div>
    <div v-for="calendar in this.dataArray" :key="calendar.index">
      <h3> Financial Year : {{calendar.financialYearStart}}-{{calendar.financialYearEnd}} </h3>
      <h4> Periods:</h4>
      <table class="table">
        <thead>
          <tr>
            <td>Start</td>
            <td>End</td>
            <td>Period Number</td>
          </tr>
        </thead>
        <tbody v-for="month in calendar.months" :key="month.index">
        <tr>
          <td>{{month.startDate}}</td>
          <td>{{month.endDate}}</td>
          <td>{{month.periodNumber}}</td>
        </tr>
        </tbody>
      </table>
  </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import axios from 'axios'

export default defineComponent({
  name: 'FinancialCalendar',
  data () {
    return {
      startDate: new Date().toISOString().slice(0, 10),
      dataArray: [],
      pageNo: 1,
      value: ''
    }
  },
  methods: {
    async getData () {
      try {
        const response = await axios.get(process.env.VUE_APP_API_URL + '?startDate=' + this.startDate + '&pageNo=' + this.pageNo)
        this.dataArray = response.data
      } catch (error) {
        console.log(error)
      }
    },
    incrementPageNo () {
      this.pageNo++
      this.getData()
    },
    resetPageNo () {
      this.pageNo = 1
      this.getData()
    }
  }
})
</script>

<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
button {
  margin-right: 5px;
}
</style>
