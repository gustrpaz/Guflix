"use client";
import '../page.css'
import Link from 'next/link'
import { api } from '@/lib/api';
import { useRouter } from 'next/navigation'

interface User {
  email: string;
  password: string;
}
export default function Login() {
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
    } catch (error) {
      alert('Senha incorreta!')
      console.error('Erro ao fazer a requisição:', error);
    }
  }
  const router = useRouter()
  return (
    <main>
      <form onSubmit={Login}>
        <div className="Form">
          <div>
            <label htmlFor="email">E-mail</label>
            <input type="email" id="email" name="email" />
          </div>
          <div>
            <label htmlFor="password">Senha</label>
            <input type="password" id="password" name="password" />
          </div>
          <div>
            <button type="submit">LOGIN</button>
          </div>
        </div>
      </form>
    </main>
  )
}
