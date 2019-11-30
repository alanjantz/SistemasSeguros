Rails.application.routes.draw do
  get 'index', to: 'users#index'
  get 'main', to: 'users#main'
  post 'authenticate', to: 'users#authenticate'
  post 'create', to: 'users#create'
  # For details on the DSL available within this file, see http://guides.rubyonrails.org/routing.html
end
