<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { IdentityService } from '@/services/IdentityService';
import { useUserDataStore } from '@/stores/userDataStore';

const identityService = new IdentityService();
const router = useRouter();
const store = useUserDataStore();

const email = ref('');
const password = ref('');
const firstName = ref('');
const lastName = ref('');
const error = ref<string | null>(null);
const success = ref<string | null>(null);

const isAdmin = store.role === 'admin';

const doRegister = async () => {
  error.value = null;
  success.value = null;

  const result = await identityService.register({
    email: email.value,
    password: password.value,
    firstName: firstName.value,
    lastName: lastName.value,
  });

  if (result?.data) {
    if (!isAdmin) {
      store.jwt = result.data.jwt;
      store.refreshToken = result.data.refreshToken;
      router.push({ name: 'Home' });
    } else {
      success.value = 'Account successfully created';
      email.value = '';
      password.value = '';
      firstName.value = '';
      lastName.value = '';
    }
  } else {
    error.value = result.errors?.[0] || 'Registration failed';
  }
};
</script>

<template>
  <main class="register-wrapper">
    <div class="register-box">
      <RouterLink class="custom-logo" to="/">Apollo</RouterLink>

      <div v-if="error" class="alert alert-warning">
        {{ error }}
      </div>

      <div v-if="success" class="alert alert-success">
        {{ success }}
      </div>

      <form @submit.prevent="doRegister">
        <label for="firstName">{{ $t('Firstname') }}</label>
        <input v-model="firstName" type="text" id="firstName" required />

        <label for="lastName">{{ $t('Lastname') }}</label>
        <input v-model="lastName" type="text" id="lastName" required />

        <label for="email">{{ $t('Email') }}</label>
        <input v-model="email" type="email" id="email" required />

        <label for="password">{{ $t('Password') }}</label>
        <input v-model="password" type="password" id="password" required />

        <button type="submit" class="register-button">{{ $t('Register') }}</button>
      </form>
    </div>
  </main>
</template>

<style scoped>
.register-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 20vh;
  font-family: Arial, sans-serif;
}

.register-box {
  background-color: #1a1a1a;
  padding: 2rem;
  border-radius: 12px;
  width: 400px;
  box-shadow: 0 0 10px rgba(255, 165, 0, 0.6);
  border: 4px solid orange;
  color: white;
}

.custom-logo {
  color: orange;
  font-weight: bold;
  font-size: 2.5rem;
  text-decoration: none;
  display: block;
  text-align: center;
  margin-bottom: 1rem;
}

label {
  display: block;
  margin-top: 1rem;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 0.5rem;
  border-radius: 6px;
  border: none;
  margin-top: 0.3rem;
  background-color: #fff7e6;
  color: black;
  font-size: 1rem;
}

.register-button {
  margin-top: 2rem;
  background: linear-gradient(to top left, rgb(40, 25, 5), orange);
  border: 2px solid orange;
  border-radius: 6px;
  padding: 0.6rem;
  width: 100%;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.3s;
}

.register-button:hover {
  background: linear-gradient(to right, #e69500, #d4af37);
}

.alert {
  background-color: #ffcccc;
  color: black;
  padding: 0.75rem;
  border-radius: 6px;
  margin-bottom: 1rem;
  text-align: center;
}

.alert-success {
  background-color: #ccffcc;
  color: black;
  padding: 0.75rem;
  border-radius: 6px;
  margin-bottom: 1rem;
  text-align: center;
}
</style>
