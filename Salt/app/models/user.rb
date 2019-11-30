require 'digest/md5'
class User < ApplicationRecord
	before_create :salt_password

  def self.authenticate(user, password)
		user = User.find_by(user: user)
		return false if user.blank?
		return user.password == Digest::MD5.hexdigest(user.salt + password)
  end


  def salt_password
		self.salt = DateTime.now.to_s(:number)
		self.password = Digest::MD5.hexdigest(salt + self.password)
	end
end
