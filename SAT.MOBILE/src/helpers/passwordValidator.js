export function passwordValidator(password) {
  if (!password) return "Favor informar a senha."
  if (password.length < 6) return 'A senha precisa ter pelo menos 6 caracteres.'
  return ''
}
