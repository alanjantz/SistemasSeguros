class AddTableUsers < ActiveRecord::Migration[5.1]
  def change
    create_table  :users do |t|
      t.string :user 
      t.string :password
      t.string :salt
    end
  end
end
