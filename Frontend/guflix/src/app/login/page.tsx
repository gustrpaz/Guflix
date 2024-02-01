"use client";
import '../page.css'
import Link from 'next/link'
import { api } from '@/lib/api';
import { useRouter } from 'next/navigation'
import { useEffect, useState } from "react";

interface User {
  email: string;
  password: string;
}
export default function Login() {
  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem('token');
      return !!token;

    };
    if (checkAuth()) {
      router.replace('/filme');
    }
  },);
  async function Login(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const email = (event.target as any).email.value;
    const password = (event.target as any).password.value;
    try {
      const user: User = {
        email,
        password
      };
      const response = await api.post('/login', user);
      console.log(`token= ${response.data.token}`)
      router.push('/filme')
      localStorage.setItem('token', response.data.token);
      window.location.reload();
    } catch (error) {
      alert('Senha incorreta!')
      console.error('Erro ao fazer a requisição:', error);
    }
  }
  const router = useRouter()
  return (
    <form onSubmit={Login}>
      <h1><u>Login</u></h1>
      <div className='form-content login'>
        <div className="form">
          <label htmlFor="email">E-mail</label>
          <input type="email" id="email" name="email" />
        </div>
        <div className="form">
          <label htmlFor="password">Senha</label>
          <input type="password" id="password" name="password" />
        </div>
        <div>
          <button className="btn" type="submit">LOGIN</button>
        </div>
        <Link href={'/'}><u>Criar conta</u></Link>
      </div>
    </form>

  )
}
