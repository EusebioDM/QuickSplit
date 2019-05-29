package org.quicksplit.adapters;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import org.quicksplit.R;
import org.quicksplit.Utils;
import org.quicksplit.models.User;

import java.util.List;

public class AvatarAdapter extends RecyclerView.Adapter<AvatarAdapter.AvatarViewHolder> {

    private List<User> users;
    private OnItemClickListener mListener;

    public interface OnItemClickListener {
    }

    public void setOnItemClickListener(OnItemClickListener listener) {
        this.mListener = listener;
    }

    @NonNull
    @Override
    public AvatarViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.item_avatar, viewGroup, false);
        AvatarViewHolder friendsViewHolder = new AvatarViewHolder(view, mListener, users);
        return friendsViewHolder;
    }

    @Override
    public void onBindViewHolder(@NonNull AvatarViewHolder groupViewHolder, int i) {
        final User currentItem = users.get(i);
        groupViewHolder.mImageAvatar.setImageBitmap(Utils.stringToBitMap(currentItem.getAvatar()));
    }

    @Override
    public int getItemCount() {
        return users.size();
    }

    public AvatarAdapter(List<User> users) {
        this.users = users;
    }

    public static class AvatarViewHolder extends RecyclerView.ViewHolder {

        public ImageView mImageAvatar;

        public AvatarViewHolder(@NonNull View itemView, final OnItemClickListener listener, final List<User> users) {
            super(itemView);
            mImageAvatar = itemView.findViewById(R.id.imageView);
        }
    }
}
