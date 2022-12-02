<template>
  <form @submit.prevent="submitForm">
    <div class="form-control" :class="{invalid: !firstName.isValid}">
      <label for="firstname">Firstname</label>
      <input
        type="text"
        id="firstname"
        v-model.trim="firstName.val"
        @blur="clearValidity('firstName')"
      />
      <p v-if="!firstName.isValid">Firstname must not be empty.</p>
    </div>
    <div class="form-control" :class="{invalid: !lastName.isValid}">
      <label for="lastname">Lastname</label>
      <input
        type="text"
        id="lastname"
        v-model.trim="lastName.val"
        @blur="clearValidity('lastName')"
      />
      <p v-if="!lastName.isValid">Lastname must not be empty.</p>
    </div>
    <div class="form-control" :class="{invalid: !email.isValid}">
      <label for="email">Email</label>
      <input
        type="text"
        id="firstname"
        v-model.trim="email.val"
        @blur="clearValidity('email')"
      />
      <p v-if="!firstName.isValid">Email must not be empty.</p>
    </div>    
    <div class="form-control" :class="{invalid: !salary.isValid}">
      <label for="salary">Monthly Salary</label>
      <input type="number" id="salary" v-model.number="salary.val" @blur="clearValidity('salary')" />
      <p v-if="!salary.isValid">Salary must be greater than 0.</p>
    </div>   
    <p v-if="!formIsValid">Please fix the above errors and submit again.</p>
    <base-button>{{getButtonActionLabel}}</base-button>
  </form>
</template>

<script>
export default {
  emits: ['save-data'],
  props: ['id'],
  data() {
    return {     
      selectedEmployee: null, 
      firstName: {
        val: '',
        isValid: true,
      },
      lastName: {
        val: '',
        isValid: true,
      },
      email: {
        val: '',
        isValid: true,
      },
      salary: {
        val: null,
        isValid: true,
      },
      formIsValid: true,
    };
  },
  computed: {
    getButtonActionLabel() {
      return this.selectedEmployee != null ? "Save" : "Register"
    }
  },
  methods: {
    clearValidity(input) {
      this[input].isValid = true;
    },
    validateForm() {
      this.formIsValid = true;
      if (this.firstName.val === '') {
        this.firstName.isValid = false;
        this.formIsValid = false;
      }
      if (this.lastName.val === '') {
        this.lastName.isValid = false;
        this.formIsValid = false;
      }
      if (this.email.val === '') {
        this.email.isValid = false;
        this.formIsValid = false;
      }
      if (!this.salary.val || this.salary.val < 0) {
        this.salary.isValid = false;
        this.formIsValid = false;
      }
    },
    submitForm() {
      this.validateForm();

      if (!this.formIsValid) {
        return;
      }

      let formData = {
        firstName: this.firstName.val,
        lastName: this.lastName.val,
        email: this.email.val,
        monthlySalary: this.salary.val
      };

      if(this.id != null) 
      {   
        formData = {
          ...formData,
          id: this.id
        }
      }

      this.$emit('save-data', formData);
    },
  },
  created() {
    if(this.id != null) 
    {   
      this.selectedEmployee = this.$store.getters['employees/employees'].find(
        (employee) => employee.id === this.id
      );
      this.firstName.val = this.selectedEmployee.firstName;
      this.lastName.val = this.selectedEmployee.lastName;
      this.email.val = this.selectedEmployee.email;
      this.salary.val = this.selectedEmployee.monthlySalary;
    }
  }
};
</script>

<style scoped>
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input[type='checkbox'] + label {
  font-weight: normal;
  display: inline;
  margin: 0 0 0 0.5rem;
}

input,
textarea {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
}

input:focus,
textarea:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

input[type='checkbox'] {
  display: inline;
  width: auto;
  border: none;
}

input[type='checkbox']:focus {
  outline: #3d008d solid 1px;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}

.invalid label {
  color: red;
}

.invalid input,
.invalid textarea {
  border: 1px solid red;
}
</style>