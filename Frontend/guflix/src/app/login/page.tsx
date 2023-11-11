"use client";
import '../page.css'
import Link from 'next/link'
import { api } from '@/lib/api';
interface User {
  email: string;
  password: string;
}
async function logar(event: React.FormEvent<HTMLFormElement>) {
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
  } catch (error) {
    console.error('Erro ao fazer a requisição:', error);
  }
}
export default function Login() {
  return (
    <main>
      <form onSubmit={logar}>
        <div className="Form">
          <div>
            <label htmlFor="email">E-mail</label>
            <input type="email" id="email" name="email"/>
          </div>
          <div>
            <label htmlFor="password">Senha</label>
            <input type="password" id="password" name="password" />
          </div>
          <div>
            <Link href="/filme">
              <button type="submit">LOGIN</button>
            </Link>
          </div>
        </div>
      </form>
    </main>
  )
}