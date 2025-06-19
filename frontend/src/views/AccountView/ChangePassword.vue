<script setup lang="ts">
import {useUserDataStore} from "@/stores/userDataStore.ts";
import {IdentityService} from "@/services/IdentityService.ts";
import {ref} from "vue";
import {RouterLink} from "vue-router";

const store = useUserDataStore();
const identityService = new IdentityService();

const message = ref('');
const currentPassword = ref('');
const newPassword = ref('');
const confirmNewPassword = ref('');

const changePassword = async () => {
  if (newPassword.value !== confirmNewPassword.value) {
    message.value = 'Uus parool ja kinnitus ei ühti!';
    return;
  }

  const result = await identityService.changePassword({
    email: store.email!,
    currentPassword: currentPassword.value,
    newPassword: newPassword.value
  });

  message.value = result.errors
    ? `Viga: ${result.errors.join(', ')}`
    : 'Parool muudetud edukalt!';
};

</script>

<template>

  <div class="page-wrapper">

    <!-- Parem paneel -->
    <div class="edit-panel">
      <h2>{{ $t('Change password') }}</h2>
      <form @submit.prevent="changePassword">
        <label>{{ $t('Current password') }}</label>
        <input v-model="currentPassword" type="password" :placeholder="$t('Enter current password')" />

        <label>{{ $t('New password') }}</label>
        <input v-model="newPassword" type="password" :placeholder="$t('Enter new password')" />

        <label>{{ $t('Confirm new password') }}</label>
        <input v-model="confirmNewPassword" type="password" :placeholder="$t('Confirm new password')" />

        <div class="buttons">
          <button type="submit" class="save-button">{{ $t('Change password') }}</button>
          <router-link to="/account" class="change-password">
            {{ $t('Back') }}
          </router-link>
        </div>

        <p class="message">{{ message }}</p>

      </form>
    </div>
  </div>

</template>

<style scoped>
.page-wrapper {
  height: 80vh;
  display: flex;
  justify-content: center;
  align-items: center;
  font-family: "Segoe UI", sans-serif;
}

.edit-panel {
  background-color: #1a1a1a;
  color: white;
  border-radius: 16px;
  padding: 2rem 3rem;
  box-shadow: 0 0 10px rgba(255, 165, 0, 0.4);
  border: 3px solid orange;
  width: 100%;
  max-width: 600px;
  box-sizing: border-box;
  height: 550px;
}

.edit-panel h2 {
  text-align: center;
  margin-bottom: 2rem;
  font-size: 1.8rem;
  border-bottom: 1px solid orange;
  padding-bottom: 0.5rem;
}

.edit-panel form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.edit-panel label {
  font-weight: bold;
  margin-bottom: 0.2rem;
}

.edit-panel input[type="password"] {
  padding: 0.8rem;
  border-radius: 10px;
  border: 1px solid #ccc;
  background-color: #f5f5f5;
  color: black;
  font-size: 1rem;
  font-weight: normal;
  transition: border 0.2s ease;
}

.edit-panel input[type="password"]:focus {
  outline: none;
  border-color: orange;
  box-shadow: 0 0 0 2px rgba(255, 165, 0, 0.2);
}

.buttons {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 1.5rem;
}

.save-button {
  background: linear-gradient(to top left, rgb(40, 25, 5), orange);
  border: 2px solid orange;
  border-radius: 8px;
  padding: 0.6rem 1.4rem;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.3s ease;
}

.save-button:hover {
  background: linear-gradient(to right, #e69500, #d4af37);
}

.change-password {
  color: orange;
  text-decoration: underline;
  font-weight: bold;
  cursor: pointer;
  background: none;
  border: none;
  padding: 0;
  transition: color 0.3s ease;
}

.change-password:hover {
  color: #ffcc80;
}

.message {
  margin-top: 1rem;
  color: lightgreen;
  font-weight: bold;
  text-align: center;
}
</style>
