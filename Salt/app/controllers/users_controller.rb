class UsersController < ApplicationController
  
  def index
  end

  def authenticate
    if User.authenticate(params[:user], params[:password])
      redirect_to :action => 'main'
    else
      redirect_to :action => 'index'
    end
  end

  def main
  end

  def create
    User.create(user_params)
  end

  private
  def user_params
    params.permit(:user, :password)
  end
end
